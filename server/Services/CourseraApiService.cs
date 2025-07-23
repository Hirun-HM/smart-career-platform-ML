using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Services
{
    public class CourseraApiService
    {
        private const string CourseraOAuthUrl = "https://api.coursera.org/oauth2/client_credentials/token";
        private const string CourseraApiBaseUrl = "https://api.coursera.com/ent";

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CourseraApiService> _logger;
        private readonly IMemoryCache _cache;
        private string? _accessToken;
        private DateTime _tokenExpiry;

        public CourseraApiService(
            HttpClient httpClient, 
            IConfiguration configuration, 
            ILogger<CourseraApiService> logger,
            IMemoryCache cache)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            _cache = cache;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            if (!string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _tokenExpiry)
            {
                return _accessToken;
            }

            var apiKey = _configuration["Coursera:ApiKey"];
            var apiSecret = _configuration["Coursera:ApiSecret"];
            
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
            {
                throw new InvalidOperationException("Coursera API credentials not configured");
            }

            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}"));
            
            var request = new HttpRequestMessage(HttpMethod.Post, CourseraOAuthUrl);
            request.Headers.Add("Authorization", $"Basic {credentials}");
            request.Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            try
            {
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<CourseraTokenResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (tokenResponse?.AccessToken == null)
                {
                    throw new InvalidOperationException("Failed to obtain access token from Coursera API");
                }

                _accessToken = tokenResponse.AccessToken;
                _tokenExpiry = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn - 60);

                _logger.LogInformation("Successfully obtained Coursera access token");
                return _accessToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to obtain Coursera access token");
                throw;
            }
        }

        public async Task<List<CourseData>> SearchCoursesAsync(string query, int limit = 50)
        {
            var cacheKey = $"coursera_search_{query}_{limit}";
            
            if (_cache.TryGetValue(cacheKey, out List<CourseData>? cachedCourses))
            {
                return cachedCourses ?? new List<CourseData>();
            }

            try
            {
                var token = await GetAccessTokenAsync();
                
                var requestUrl = $"https://api.coursera.org/api/courses.v1?q=search&query={Uri.EscapeDataString(query)}&limit={limit}&fields=name,description,slug,photoUrl,workload,averageRating,instructorIds,partnerIds,domainTypes";
                
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                request.Headers.Add("Authorization", $"Bearer {token}");

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var coursesResponse = JsonSerializer.Deserialize<CourseraSearchResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var courses = coursesResponse?.Elements?.Select(MapToCourseData).ToList() ?? new List<CourseData>();
                
                // Cache for 1 hour
                _cache.Set(cacheKey, courses, TimeSpan.FromHours(1));
                
                return courses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to search Coursera courses for query: {Query}", query);
                return new List<CourseData>();
            }
        }

        public async Task<List<CourseData>> GetCoursesForSkillsAsync(List<string> skills, int limitPerSkill = 10)
        {
            var allCourses = new List<CourseData>();

            foreach (var skill in skills.Take(5)) // Limit to 5 skills to avoid API limits
            {
                try
                {
                    var courses = await SearchCoursesAsync(skill, limitPerSkill);
                    allCourses.AddRange(courses);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to get courses for skill: {Skill}", skill);
                }
            }

            // Remove duplicates and return
            return allCourses
                .GroupBy(c => c.Id)
                .Select(g => g.First())
                .Take(50)
                .ToList();
        }

        public async Task<List<CourseData>> GetPopularCoursesAsync(string category = "", int limit = 20)
        {
            var cacheKey = $"coursera_popular_{category}_{limit}";
            
            if (_cache.TryGetValue(cacheKey, out List<CourseData>? cachedCourses))
            {
                return cachedCourses ?? new List<CourseData>();
            }

            try
            {
                var token = await GetAccessTokenAsync();
                
                var requestUrl = string.IsNullOrEmpty(category) 
                    ? $"https://api.coursera.org/api/courses.v1?limit={limit}&fields=name,description,slug,photoUrl,workload,averageRating,instructorIds,partnerIds,domainTypes"
                    : $"https://api.coursera.org/api/courses.v1?q=search&query={Uri.EscapeDataString(category)}&limit={limit}&fields=name,description,slug,photoUrl,workload,averageRating,instructorIds,partnerIds,domainTypes";
                
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                request.Headers.Add("Authorization", $"Bearer {token}");

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var coursesResponse = JsonSerializer.Deserialize<CourseraSearchResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var courses = coursesResponse?.Elements?.Select(MapToCourseData).ToList() ?? new List<CourseData>();
                
                // Cache for 2 hours
                _cache.Set(cacheKey, courses, TimeSpan.FromHours(2));
                
                return courses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get popular Coursera courses");
                return new List<CourseData>();
            }
        }

        public async Task<List<CourseData>> GetAllCoursesAsync(string orgId)
        {
            try
            {
                await EnsureValidTokenAsync();
                
                var cacheKey = $"coursera_all_courses_{orgId}";
                if (_cache.TryGetValue(cacheKey, out List<CourseData>? cachedCourses))
                {
                    return cachedCourses ?? new List<CourseData>();
                }

                var requestUrl = $"{CourseraApiBaseUrl}/api/businesses.v1/{orgId}/contents?limit=100";
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

                var response = await _httpClient.SendAsync(request);
                
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var courses = ParseCoursesFromJson(json);
                    
                    _cache.Set(cacheKey, courses, TimeSpan.FromHours(4));
                    return courses;
                }
                else
                {
                    _logger.LogError("Failed to fetch courses from Coursera API: {StatusCode}", response.StatusCode);
                    return new List<CourseData>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all courses from Coursera API");
                return new List<CourseData>();
            }
        }

        public async Task<List<CourseData>> GetSkillBasedRecommendationsAsync(string orgId, string programId, List<string> skillIds)
        {
            try
            {
                await EnsureValidTokenAsync();
                
                var cacheKey = $"coursera_skill_recs_{orgId}_{programId}_{string.Join(",", skillIds)}";
                if (_cache.TryGetValue(cacheKey, out List<CourseData>? cachedRecs))
                {
                    return cachedRecs ?? new List<CourseData>();
                }

                var allCourses = new List<CourseData>();
                
                // Get recommendations for each skill
                foreach (var skillId in skillIds.Take(5)) // Limit to avoid API rate limits
                {
                    var requestUrl = $"{CourseraApiBaseUrl}/api/rest/v1/enterprise/programs/{programId}/skillsets/{skillId}/recommendations?limit=20";
                    var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

                    var response = await _httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var skillCourses = ParseSkillRecommendationsFromJson(json);
                        allCourses.AddRange(skillCourses);
                    }
                }

                // Remove duplicates and limit results
                var uniqueCourses = allCourses
                    .GroupBy(c => c.Id)
                    .Select(g => g.First())
                    .Take(50)
                    .ToList();

                _cache.Set(cacheKey, uniqueCourses, TimeSpan.FromHours(2));
                return uniqueCourses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching skill-based recommendations from Coursera API");
                return new List<CourseData>();
            }
        }

        private async Task EnsureValidTokenAsync()
        {
            if (string.IsNullOrEmpty(_accessToken) || DateTime.UtcNow >= _tokenExpiry)
            {
                await GetAccessTokenAsync();
            }
        }

        private CourseData MapToCourseData(JsonElement element)
        {
            var course = new CourseData();

            try
            {
                course.Id = element.TryGetProperty("id", out var idProp) ? 
                    int.TryParse(idProp.GetString(), out var id) ? id : 0 : 0;

                course.Title = element.TryGetProperty("name", out var nameProp) ? 
                    nameProp.GetString() ?? "" : "";

                course.Description = element.TryGetProperty("description", out var descProp) ? 
                    descProp.GetString() ?? "" : "";

                // Extract skills from domain types or course content
                course.Skills = ExtractSkillsFromCourse(element);

                course.Level = DetermineLevel(element);
                course.Category = ExtractCategory(element);
                
                course.Rating = element.TryGetProperty("averageRating", out var ratingProp) ? 
                    (decimal)ratingProp.GetDouble() : 0m;

                course.Duration = element.TryGetProperty("workload", out var workloadProp) ? 
                    ParseDuration(workloadProp.GetString()) : 0;

                course.Provider = "Coursera";
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error mapping Coursera course data");
            }

            return course;
        }

        private List<string> ExtractSkillsFromCourse(JsonElement element)
        {
            var skills = new List<string>();

            // Try to extract from domain types
            if (element.TryGetProperty("domainTypes", out var domainProp) && 
                domainProp.ValueKind == JsonValueKind.Array)
            {
                foreach (var domain in domainProp.EnumerateArray())
                {
                    var domainName = domain.TryGetProperty("domainId", out var domainIdProp) ? 
                        domainIdProp.GetString() : null;
                    if (!string.IsNullOrEmpty(domainName))
                    {
                        skills.Add(domainName);
                    }
                }
            }

            // Extract from course name and description
            var courseName = element.TryGetProperty("name", out var nameProp) ? 
                nameProp.GetString() ?? "" : "";
            var description = element.TryGetProperty("description", out var descProp) ? 
                descProp.GetString() ?? "" : "";

            skills.AddRange(ExtractSkillsFromText($"{courseName} {description}"));

            return skills.Distinct().Take(10).ToList();
        }

        private List<string> ExtractSkillsFromText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new List<string>();

            var skillKeywords = new Dictionary<string, string[]>
            {
                ["Programming"] = new[] { "python", "java", "javascript", "c#", "cpp", "ruby", "go", "rust" },
                ["Web Development"] = new[] { "html", "css", "react", "angular", "vue", "node.js", "express" },
                ["Data Science"] = new[] { "machine learning", "data analysis", "statistics", "pandas", "numpy", "tensorflow", "pytorch" },
                ["Cloud"] = new[] { "aws", "azure", "google cloud", "docker", "kubernetes" },
                ["Database"] = new[] { "sql", "mongodb", "postgresql", "mysql", "redis" },
                ["Mobile"] = new[] { "android", "ios", "react native", "flutter", "swift", "kotlin" },
                ["AI/ML"] = new[] { "artificial intelligence", "deep learning", "neural networks", "nlp" },
                ["DevOps"] = new[] { "ci/cd", "jenkins", "git", "docker", "kubernetes", "terraform" }
            };

            var foundSkills = new List<string>();
            var textLower = text.ToLower();

            foreach (var category in skillKeywords)
            {
                foreach (var keyword in category.Value)
                {
                    if (textLower.Contains(keyword))
                    {
                        foundSkills.Add(keyword);
                    }
                }
            }

            return foundSkills.Take(5).ToList();
        }

        private string DetermineLevel(JsonElement element)
        {
            var name = element.TryGetProperty("name", out var nameProp) ? 
                nameProp.GetString()?.ToLower() ?? "" : "";
            var description = element.TryGetProperty("description", out var descProp) ? 
                descProp.GetString()?.ToLower() ?? "" : "";

            var text = $"{name} {description}";

            if (text.Contains("beginner") || text.Contains("introduction") || text.Contains("basics"))
                return "Beginner";
            if (text.Contains("advanced") || text.Contains("expert") || text.Contains("master"))
                return "Advanced";
            
            return "Intermediate";
        }

        private string ExtractCategory(JsonElement element)
        {
            if (element.TryGetProperty("domainTypes", out var domainProp) && 
                domainProp.ValueKind == JsonValueKind.Array)
            {
                var firstDomain = domainProp.EnumerateArray().FirstOrDefault();
                if (firstDomain.ValueKind != JsonValueKind.Undefined)
                {
                    return firstDomain.TryGetProperty("domainId", out var domainIdProp) ? 
                        domainIdProp.GetString() ?? "General" : "General";
                }
            }

            return "General";
        }

        private int ParseDuration(string? workload)
        {
            if (string.IsNullOrEmpty(workload))
                return 0;

            // Extract numbers from workload string (e.g., "4-6 hours per week" -> 5 hours)
            var numbers = System.Text.RegularExpressions.Regex.Matches(workload, @"\d+")
                .Cast<System.Text.RegularExpressions.Match>()
                .Select(m => int.Parse(m.Value))
                .ToArray();

            if (numbers.Length == 0)
                return 0;

            if (numbers.Length == 1)
                return numbers[0];

            // If range, take average
            return (int)numbers.Average();
        }

        private List<CourseData> ParseCoursesFromJson(string json)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var response = JsonSerializer.Deserialize<CourseraApiResponse>(json, options);
                var courses = response?.Elements?.Select(MapToCourseData).ToList() ?? new List<CourseData>();

                return courses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing courses JSON from Coursera API");
                return new List<CourseData>();
            }
        }

        private List<CourseData> ParseSkillRecommendationsFromJson(string json)
        {
            try
            {
                var doc = JsonDocument.Parse(json);
                var courses = new List<CourseData>();
                
                if (doc.RootElement.TryGetProperty("elements", out var elements))
                {
                    foreach (var element in elements.EnumerateArray())
                    {
                        if (element.TryGetProperty("contentRecommendation", out var contentRec))
                        {
                            var course = ParseCourseFromElement(contentRec);
                            if (course != null)
                            {
                                courses.Add(course);
                            }
                        }
                    }
                }
                
                return courses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing skill recommendations JSON");
                return new List<CourseData>();
            }
        }

        private CourseData? ParseCourseFromElement(JsonElement element)
        {
            try
            {
                if (!element.TryGetProperty("id", out var idProp)) return null;
                
                var course = new CourseData
                {
                    Id = int.TryParse(idProp.GetString(), out var id) ? id : 0,
                    Title = element.TryGetProperty("name", out var titleProp) ? titleProp.GetString() ?? "" : "",
                    Description = element.TryGetProperty("description", out var descProp) ? descProp.GetString() ?? "" : "",
                    Provider = "Coursera",
                    Category = ExtractCategory(element),
                    Level = DetermineLevel(element),
                    Skills = ExtractSkillsFromElement(element),
                    Duration = ExtractDuration(element),
                    Rating = ExtractRating(element)
                };
                
                return course;
            }
            catch
            {
                return null;
            }
        }

        private List<string> ExtractSkillsFromElement(JsonElement element)
        {
            var skills = new List<string>();
            
            if (element.TryGetProperty("skills", out var skillsArray))
            {
                foreach (var skill in skillsArray.EnumerateArray())
                {
                    if (skill.TryGetProperty("name", out var nameProperty))
                    {
                        var skillName = nameProperty.GetString();
                        if (!string.IsNullOrEmpty(skillName))
                        {
                            skills.Add(skillName);
                        }
                    }
                }
            }
            
            return skills;
        }

        private int ExtractDuration(JsonElement element)
        {
            if (element.TryGetProperty("workload", out var workloadProp))
            {
                return ParseDuration(workloadProp.GetString());
            }
            return 0;
        }

        private decimal ExtractRating(JsonElement element)
        {
            if (element.TryGetProperty("averageRating", out var ratingProp) && 
                ratingProp.TryGetDecimal(out var rating))
            {
                return rating;
            }
            return 0m;
        }
    }

    public class CourseraTokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
    }

    public class CourseraSearchResponse
    {
        public JsonElement[]? Elements { get; set; }
        public CourseeraPaging? Paging { get; set; }
    }

    public class CourseeraPaging
    {
        public string? Next { get; set; }
        public int Total { get; set; }
    }

    public class CourseraApiResponse
    {
        public JsonElement[]? Elements { get; set; }
    }

    public class CourseraSkillRecommendationResponse
    {
        public JsonElement[]? Elements { get; set; }
    }
}