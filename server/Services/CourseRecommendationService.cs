using Microsoft.EntityFrameworkCore;
using SmartCareerPlatform.Models;
using SmartCareerPlatform.Repository;
using System.Text.Json;

namespace SmartCareerPlatform.Services
{
    public class CourseRecommendationService : ICourseRecommendationService
    {
        private const string DefaultMlServiceUrl = "http://localhost:5001";
        private const string InternalProvider = "Internal";
        private const string CourseraProvider = "Coursera";
        private const string MlServiceUrlKey = "MLServiceUrl";

        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserCourseInteractionRepository _interactionRepository;
        private readonly CourseraApiService _courseraService;
        private readonly ILogger<CourseRecommendationService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CourseRecommendationService(
            ICourseRepository courseRepository,
            IUserRepository userRepository,
            IUserCourseInteractionRepository interactionRepository,
            CourseraApiService courseraService,
            ILogger<CourseRecommendationService> logger,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _interactionRepository = interactionRepository;
            _courseraService = courseraService;
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<CourseRecommendationResponse>> GetPersonalizedRecommendationsAsync(int userId)
        {
            try
            {
                var userProfile = await GetUserLearningProfileAsync(userId);
                
                // Get available courses from both internal and Coursera
                var internalCourses = await GetInternalCoursesAsync();
                var courseraCourses = await _courseraService.GetCoursesForSkillsAsync(userProfile.Skills);
                
                var allCourses = new List<CourseData>();
                allCourses.AddRange(internalCourses);
                allCourses.AddRange(courseraCourses);

                // Get ML recommendations
                var mlRequest = new MLRecommendationRequest
                {
                    UserId = userId,
                    UserSkills = userProfile.Skills,
                    UserInterests = userProfile.Interests,
                    Experience = userProfile.Experience,
                    AvailableCourses = allCourses
                };

                var mlRecommendations = await GetMLRecommendationsAsync(mlRequest);
                
                // Convert to response format
                var recommendations = new List<CourseRecommendationResponse>();
                
                foreach (var mlRec in mlRecommendations.Recommendations.Take(20))
                {
                    var course = allCourses.FirstOrDefault(c => c.Id == mlRec.CourseId);
                    if (course != null)
                    {
                        var recommendation = MapToRecommendationResponse(course, mlRec, userProfile.Skills);
                        recommendations.Add(recommendation);
                    }
                }

                return recommendations.OrderByDescending(r => r.RecommendationScore).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting personalized recommendations for user {UserId}", userId);
                return new List<CourseRecommendationResponse>();
            }
        }

        public async Task<List<CourseRecommendationResponse>> GetRecommendationsWithPreferencesAsync(CourseRecommendationRequest request)
        {
            try
            {
                var userProfile = await GetUserLearningProfileAsync(request.UserId);
                
                // Get courses based on preferences
                var internalCourses = await GetFilteredInternalCoursesAsync(request);
                var courseraCourses = new List<CourseData>();

                if (request.IncludeExternalCourses)
                {
                    var searchQuery = string.Join(" ", request.TargetSkills.Union(userProfile.Skills).Take(3));
                    courseraCourses = await _courseraService.SearchCoursesAsync(searchQuery, request.Limit);
                }

                var allCourses = new List<CourseData>();
                allCourses.AddRange(internalCourses);
                allCourses.AddRange(courseraCourses);

                // Apply filters
                allCourses = ApplyFilters(allCourses, request);

                // Get ML recommendations
                var mlRequest = new MLRecommendationRequest
                {
                    UserId = request.UserId,
                    UserSkills = userProfile.Skills,
                    UserInterests = userProfile.Interests,
                    Experience = userProfile.Experience,
                    AvailableCourses = allCourses,
                    TargetSkills = request.TargetSkills,
                    CareerGoal = request.CareerGoal
                };

                var mlRecommendations = await GetMLRecommendationsAsync(mlRequest);
                
                var recommendations = new List<CourseRecommendationResponse>();
                
                foreach (var mlRec in mlRecommendations.Recommendations.Take(request.Limit))
                {
                    var course = allCourses.FirstOrDefault(c => c.Id == mlRec.CourseId);
                    if (course != null)
                    {
                        var recommendation = MapToRecommendationResponse(course, mlRec, userProfile.Skills);
                        recommendations.Add(recommendation);
                    }
                }

                return recommendations.OrderByDescending(r => r.RecommendationScore).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting recommendations with preferences for user {UserId}", request.UserId);
                return new List<CourseRecommendationResponse>();
            }
        }

        public async Task<List<CourseRecommendationResponse>> GetSkillBasedRecommendationsAsync(int userId, List<string> targetSkills)
        {
            try
            {
                var userProfile = await GetUserLearningProfileAsync(userId);
                
                // Get courses that teach the target skills
                var courseraCourses = await _courseraService.GetCoursesForSkillsAsync(targetSkills);
                var internalCourses = await GetInternalCoursesBySkillsAsync(targetSkills);
                
                var allCourses = new List<CourseData>();
                allCourses.AddRange(internalCourses);
                allCourses.AddRange(courseraCourses);

                // Score courses based on skill relevance
                var recommendations = new List<CourseRecommendationResponse>();
                
                foreach (var course in allCourses)
                {
                    var skillMatch = CalculateSkillMatch(course.Skills, targetSkills);
                    if (skillMatch > 0.1m) // Only include courses with reasonable skill match
                    {
                        var recommendation = new CourseRecommendationResponse
                        {
                            Id = course.Id,
                            Title = course.Title,
                            Description = course.Description,
                            Skills = course.Skills,
                            Duration = course.Duration,
                            Rating = course.Rating,
                            Category = course.Category,
                            Level = course.Level,
                            Provider = course.Provider,
                            RecommendationScore = skillMatch,
                            RecommendationReason = $"Teaches {string.Join(", ", course.Skills.Intersect(targetSkills))}",
                            MatchingSkills = course.Skills.Intersect(userProfile.Skills).ToList(),
                            NewSkillsToLearn = course.Skills.Intersect(targetSkills).ToList(),
                            IsExternal = course.Provider != "Internal"
                        };
                        
                        recommendations.Add(recommendation);
                    }
                }

                return recommendations
                    .OrderByDescending(r => r.RecommendationScore)
                    .Take(15)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting skill-based recommendations for user {UserId}", userId);
                return new List<CourseRecommendationResponse>();
            }
        }

        public async Task<List<CourseRecommendationResponse>> GetCareerPathRecommendationsAsync(int userId, string careerGoal)
        {
            try
            {
                var userProfile = await GetUserLearningProfileAsync(userId);
                
                // Get required skills for the career goal
                var requiredSkills = await GetRequiredSkillsForCareerAsync(careerGoal);
                var missingSkills = requiredSkills.Except(userProfile.Skills).ToList();
                
                if (!missingSkills.Any())
                {
                    return new List<CourseRecommendationResponse>();
                }

                // Get courses for missing skills
                return await GetSkillBasedRecommendationsAsync(userId, missingSkills);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting career path recommendations for user {UserId}", userId);
                return new List<CourseRecommendationResponse>();
            }
        }

        public async Task<MLRecommendationResponse> GetMLRecommendationsAsync(MLRecommendationRequest request)
        {
            try
            {
                // Call ML service for recommendations
                var mlServiceUrl = _configuration[MlServiceUrlKey] ?? DefaultMlServiceUrl;
                
                // Prepare request for the new ML service format
                var mlServiceRequest = new
                {
                    user_id = request.UserId,
                    user_skills = request.UserSkills,
                    user_experience = request.Experience,
                    limit = 20
                };
                
                var response = await _httpClient.PostAsJsonAsync($"{mlServiceUrl}/recommend-courses", mlServiceRequest);
                
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(json);
                    
                    // Parse the response from the Python ML service
                    var recommendations = new List<CourseRecommendationItem>();
                    
                    if (doc.RootElement.TryGetProperty("recommendations", out var recsArray) && recsArray.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var rec in recsArray.EnumerateArray())
                        {
                            if (rec.TryGetProperty("course_id", out var courseIdProp) &&
                                rec.TryGetProperty("recommendation_score", out var scoreProp))
                            {
                                recommendations.Add(new CourseRecommendationItem
                                {
                                    CourseId = courseIdProp.GetInt32(),
                                    Score = (decimal)scoreProp.GetDouble(),
                                    Reason = rec.TryGetProperty("recommendation_reason", out var reason) ? 
                                            reason.GetString() ?? "Based on your profile" : "Based on your profile"
                                });
                            }
                        }
                    }
                    
                    return new MLRecommendationResponse 
                    { 
                        Recommendations = recommendations 
                    };
                }
                else
                {
                    _logger.LogWarning("ML service returned error: {StatusCode}", response.StatusCode);
                    return await GetFallbackRecommendationsAsync(request);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling ML service for recommendations");
                return await GetFallbackRecommendationsAsync(request);
            }
        }

        public async Task<UserLearningProfile> GetUserLearningProfileAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return new UserLearningProfile { UserId = userId };
                }

                var completedCourses = user.Enrollments
                    .Where(e => e.Status == "Completed")
                    .Select(e => e.Course.Title)
                    .ToList();

                return new UserLearningProfile
                {
                    UserId = userId,
                    Skills = user.Skills.Select(s => s.Name).ToList(),
                    Interests = user.Interests,
                    Experience = user.Experience,
                    CompletedCourses = completedCourses,
                    SkillProficiency = CalculateSkillProficiency(user),
                    CareerGoals = ExtractCareerGoals(user)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user learning profile for user {UserId}", userId);
                return new UserLearningProfile { UserId = userId };
            }
        }

        public async Task<bool> RecordUserInteractionAsync(int userId, int courseId, string interactionType)
        {
            try
            {
                // Record interaction for ML training
                await _interactionRepository.AddInteractionAsync(new UserCourseInteraction {
                    UserId = userId,
                    CourseId = courseId,
                    InteractionType = interactionType, // "view", "enroll", "complete", "rate"
                    Timestamp = DateTime.UtcNow
                });

                // Send interaction to ML service asynchronously
                _ = Task.Run(() => SendInteractionToMLServiceAsync(userId, courseId, interactionType));
                
                _logger.LogInformation("Recorded user interaction: User {UserId} {InteractionType} course {CourseId}", 
                    userId, interactionType, courseId);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error recording user interaction");
                return false;
            }
        }

        public async Task TrainRecommendationModelAsync()
        {
            try
            {
                // Call ML service to retrain the model
                var mlServiceUrl = _configuration["MLService:BaseUrl"] ?? DefaultMlServiceUrl;
                var response = await _httpClient.PostAsync($"{mlServiceUrl}/train-model", null);
                
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully triggered ML model training");
                }
                else
                {
                    _logger.LogWarning("Failed to trigger ML model training: {StatusCode}", response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error triggering ML model training");
            }
        }

        public async Task<List<CourseData>> SyncCourseraCoursesAsync()
        {
            try
            {
                // Get popular courses from different categories
                var categories = new[] { "data-science", "computer-science", "business", "programming", "machine-learning" };
                var allCourses = new List<CourseData>();

                foreach (var category in categories)
                {
                    var courses = await _courseraService.GetPopularCoursesAsync(category, 20);
                    allCourses.AddRange(courses);
                }

                // Save/update courses in database
                foreach (var courseData in allCourses)
                {
                    var existingCourse = (await _courseRepository.GetAllCoursesAsync())
                        .FirstOrDefault(c => c.ExternalId == courseData.Id.ToString() && c.Provider == CourseraProvider);

                    if (existingCourse == null)
                    {
                        var newCourse = new Course
                        {
                            Title = courseData.Title,
                            Description = courseData.Description,
                            Skills = courseData.Skills,
                            Duration = courseData.Duration,
                            Rating = courseData.Rating,
                            Category = courseData.Category,
                            Level = courseData.Level,
                            Provider = CourseraProvider,
                            ExternalId = courseData.Id.ToString(),
                            IsActive = true,
                            Url = $"https://www.coursera.org/learn/{courseData.Id}",
                            CreatedAt = DateTime.UtcNow
                        };

                        await _courseRepository.AddCourseAsync(newCourse);
                    }
                    else
                    {
                        existingCourse.Title = courseData.Title;
                        existingCourse.Description = courseData.Description;
                        existingCourse.Skills = courseData.Skills;
                        existingCourse.Rating = courseData.Rating;
                        existingCourse.Duration = courseData.Duration;
                        await _courseRepository.UpdateCourseAsync(existingCourse);
                    }
                }

                _logger.LogInformation("Successfully synced {Count} Coursera courses", allCourses.Count);
                
                return allCourses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error syncing Coursera courses");
                return new List<CourseData>();
            }
        }

        // Helper methods
        private async Task<List<CourseData>> GetInternalCoursesAsync()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            var internalCourses = courses.Where(c => c.IsActive && c.Provider == InternalProvider).ToList();

            return internalCourses.Select(c => new CourseData
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Skills = c.Skills,
                Category = c.Category,
                Level = c.Level,
                Rating = c.Rating,
                Duration = c.Duration,
                Provider = c.Provider
            }).ToList();
        }

        private async Task<List<CourseData>> GetFilteredInternalCoursesAsync(CourseRecommendationRequest request)
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            var query = courses.Where(c => c.IsActive && c.Provider == InternalProvider);

            if (!string.IsNullOrEmpty(request.PreferredLevel))
            {
                query = query.Where(c => c.Level == request.PreferredLevel);
            }

            if (request.MaxDuration.HasValue)
            {
                query = query.Where(c => c.Duration <= request.MaxDuration.Value);
            }

            if (request.MinRating.HasValue)
            {
                query = query.Where(c => c.Rating >= request.MinRating.Value);
            }

            var filteredCourses = query.ToList();

            return filteredCourses.Select(c => new CourseData
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Skills = c.Skills,
                Category = c.Category,
                Level = c.Level,
                Rating = c.Rating,
                Duration = c.Duration,
                Provider = c.Provider
            }).ToList();
        }

        private async Task<List<CourseData>> GetInternalCoursesBySkillsAsync(List<string> skills)
        {
            var allCourses = await _courseRepository.GetAllCoursesAsync();
            var courses = allCourses
                .Where(c => c.IsActive && c.Provider == InternalProvider && c.Skills.Any(s => skills.Contains(s)))
                .ToList();

            return courses.Select(c => new CourseData
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Skills = c.Skills,
                Category = c.Category,
                Level = c.Level,
                Rating = c.Rating,
                Duration = c.Duration,
                Provider = c.Provider
            }).ToList();
        }

        private static List<CourseData> ApplyFilters(List<CourseData> courses, CourseRecommendationRequest request)
        {
            if (!string.IsNullOrEmpty(request.PreferredLevel))
            {
                courses = courses.Where(c => c.Level == request.PreferredLevel).ToList();
            }

            if (request.MaxDuration.HasValue)
            {
                courses = courses.Where(c => c.Duration <= request.MaxDuration.Value).ToList();
            }

            if (request.MinRating.HasValue)
            {
                courses = courses.Where(c => c.Rating >= request.MinRating.Value).ToList();
            }

            return courses;
        }

        private CourseRecommendationResponse MapToRecommendationResponse(
            CourseData course, 
            CourseRecommendationItem mlRec, 
            List<string> userSkills)
        {
            return new CourseRecommendationResponse
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Skills = course.Skills,
                Duration = course.Duration,
                Rating = course.Rating,
                Category = course.Category,
                Level = course.Level,
                Provider = course.Provider,
                RecommendationScore = mlRec.Score,
                RecommendationReason = mlRec.Reason,
                MatchingSkills = course.Skills.Intersect(userSkills).ToList(),
                NewSkillsToLearn = mlRec.RelevantSkills,
                IsExternal = course.Provider != "Internal",
                Url = course.Provider == "Coursera" ? 
                    $"https://www.coursera.org/learn/{course.Id}" : ""
            };
        }

        private static decimal CalculateSkillMatch(List<string> courseSkills, List<string> targetSkills)
        {
            if (!courseSkills.Any() || !targetSkills.Any())
                return 0m;

            var matchCount = courseSkills.Intersect(targetSkills, StringComparer.OrdinalIgnoreCase).Count();
            return (decimal)matchCount / Math.Max(courseSkills.Count, targetSkills.Count);
        }

        private static async Task<List<string>> GetRequiredSkillsForCareerAsync(string career)
        {
            // This could be enhanced with a database lookup or ML prediction
            await Task.CompletedTask; // Placeholder for actual async work
            var careerSkillsMap = new Dictionary<string, List<string>>
            {
                ["Data Scientist"] = new() { "Python", "Machine Learning", "Statistics", "SQL", "Pandas", "NumPy", "TensorFlow" },
                ["Software Developer"] = new() { "JavaScript", "Python", "Git", "SQL", "React", "Node.js", "API Development" },
                ["DevOps Engineer"] = new() { "Docker", "Kubernetes", "AWS", "CI/CD", "Linux", "Terraform", "Jenkins" },
                ["Frontend Developer"] = new() { "JavaScript", "React", "HTML5", "CSS3", "TypeScript", "Vue.js", "Responsive Design" },
                ["Backend Developer"] = new() { "Node.js", "Python", "SQL", "API Development", "MongoDB", "Express.js", "Microservices" },
                ["Mobile Developer"] = new() { "React Native", "Flutter", "iOS Development", "Android Development", "Swift", "Kotlin" }
            };

            return careerSkillsMap.ContainsKey(career) ? 
                careerSkillsMap[career] : 
                new List<string>();
        }

        private static Dictionary<string, decimal> CalculateSkillProficiency(User user)
        {
            var proficiency = new Dictionary<string, decimal>();
            
            foreach (var skill in user.Skills)
            {
                // Basic proficiency calculation based on experience and courses
                var baseScore = 0.3m; // Base proficiency for having the skill
                var experienceBonus = Math.Min(user.Experience * 0.1m, 0.5m); // Up to 0.5 from experience
                var coursesCompleted = user.Enrollments
                    .Count(e => e.Course.Skills.Contains(skill.Name) && e.Status == "Completed");
                var courseBonus = Math.Min(coursesCompleted * 0.2m, 0.4m); // Up to 0.4 from courses
                
                proficiency[skill.Name] = Math.Min(baseScore + experienceBonus + courseBonus, 1.0m);
            }
            
            return proficiency;
        }

        private static List<string> ExtractCareerGoals(User user)
        {
            // This could be enhanced to extract from user profile or preferences
            return user.Interests.Where(i => 
                i.Contains("Developer") || 
                i.Contains("Engineer") || 
                i.Contains("Scientist") || 
                i.Contains("Manager")).ToList();
        }

        private async Task<MLRecommendationResponse> GetFallbackRecommendationsAsync(MLRecommendationRequest request)
        {
            // Simple fallback recommendation logic
            await Task.CompletedTask; // Placeholder for actual async work
            var recommendations = new List<CourseRecommendationItem>();
            
            foreach (var course in request.AvailableCourses.Take(10))
            {
                var score = CalculateSimpleScore(course, request.UserSkills, request.TargetSkills);
                if (score > 0.1m)
                {
                    recommendations.Add(new CourseRecommendationItem
                    {
                        CourseId = course.Id,
                        Score = score,
                        Reason = "Based on skill matching",
                        RelevantSkills = course.Skills.Intersect(request.TargetSkills ?? new List<string>()).ToList()
                    });
                }
            }

            return new MLRecommendationResponse
            {
                Recommendations = recommendations.OrderByDescending(r => r.Score).ToList(),
                ModelConfidence = 0.7m,
                RecommendationStrategy = "Fallback"
            };
        }

        private static decimal CalculateSimpleScore(CourseData course, List<string> userSkills, List<string>? targetSkills)
        {
            var score = 0m;
            
            // Skill match score
            if (targetSkills?.Any() == true)
            {
                var skillMatch = course.Skills.Intersect(targetSkills).Count();
                score += (decimal)skillMatch / Math.Max(course.Skills.Count, targetSkills.Count) * 0.6m;
            }
            
            // User skill foundation score
            var foundationMatch = course.Skills.Intersect(userSkills).Count();
            score += (decimal)foundationMatch / Math.Max(course.Skills.Count, userSkills.Count) * 0.2m;
            
            // Rating score
            score += course.Rating / 5.0m * 0.2m;
            
            return Math.Min(score, 1.0m);
        }

        private async Task SendInteractionToMLServiceAsync(int userId, int courseId, string interactionType)
        {
            try
            {
                var userProfile = await GetUserLearningProfileAsync(userId);
                var mlServiceUrl = _configuration[MlServiceUrlKey] ?? DefaultMlServiceUrl;
                
                var interactionData = new
                {
                    user_id = userId,
                    course_id = courseId,
                    interaction_type = interactionType,
                    user_skills = userProfile.Skills,
                    user_experience = userProfile.Experience
                };

                var response = await _httpClient.PostAsJsonAsync($"{mlServiceUrl}/store-interaction", interactionData);
                
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to send interaction to ML service: {StatusCode}", response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending interaction to ML service");
            }
        }
    }
}