using Microsoft.AspNetCore.Mvc;
using SmartCareerPlatform.Models;
using SmartCareerPlatform.Services;

namespace SmartCareerPlatform.Controllers
{
    [ApiController]
    [Route("api/course-recommendations")]
    public class CourseRecommendationController : ControllerBase
    {
        private readonly ICourseRecommendationService _recommendationService;
        private readonly ILogger<CourseRecommendationController> _logger;

        public CourseRecommendationController(
            ICourseRecommendationService recommendationService,
            ILogger<CourseRecommendationController> logger)
        {
            _recommendationService = recommendationService;
            _logger = logger;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPersonalizedRecommendations(int userId)
        {
            if (userId <= 0)
                return BadRequest("Invalid user ID");

            try
            {
                var recommendations = await _recommendationService.GetPersonalizedRecommendationsAsync(userId);
                
                return Ok(new
                {
                    UserId = userId,
                    RecommendationCount = recommendations.Count,
                    Recommendations = recommendations,
                    GeneratedAt = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting personalized recommendations for user {UserId}", userId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("advanced")]
        public async Task<IActionResult> GetAdvancedRecommendations([FromBody] CourseRecommendationRequest request)
        {
            if (request.UserId <= 0)
                return BadRequest("Invalid user ID");

            try
            {
                var recommendations = await _recommendationService.GetRecommendationsWithPreferencesAsync(request);
                
                return Ok(new
                {
                    Request = request,
                    RecommendationCount = recommendations.Count,
                    Recommendations = recommendations,
                    GeneratedAt = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting advanced recommendations for user {UserId}", request.UserId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("skill-based")]
        public async Task<IActionResult> GetSkillBasedRecommendations([FromBody] SkillBasedRecommendationRequest request)
        {
            if (request.UserId <= 0)
                return BadRequest("Invalid user ID");

            if (!request.TargetSkills.Any())
                return BadRequest("Target skills are required");

            try
            {
                var recommendations = await _recommendationService.GetSkillBasedRecommendationsAsync(
                    request.UserId, request.TargetSkills);

                return Ok(new
                {
                    UserId = request.UserId,
                    TargetSkills = request.TargetSkills,
                    RecommendationCount = recommendations.Count,
                    Recommendations = recommendations,
                    GeneratedAt = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting skill-based recommendations for user {UserId}", request.UserId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("career-path")]
        public async Task<IActionResult> GetCareerPathRecommendations([FromBody] CareerPathRecommendationRequest request)
        {
            if (request.UserId <= 0)
                return BadRequest("Invalid user ID");

            if (string.IsNullOrWhiteSpace(request.CareerGoal))
                return BadRequest("Career goal is required");

            try
            {
                var recommendations = await _recommendationService.GetCareerPathRecommendationsAsync(
                    request.UserId, request.CareerGoal);

                return Ok(new
                {
                    UserId = request.UserId,
                    CareerGoal = request.CareerGoal,
                    RecommendationCount = recommendations.Count,
                    Recommendations = recommendations,
                    GeneratedAt = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting career path recommendations for user {UserId}", request.UserId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("interaction")]
        public async Task<IActionResult> RecordInteraction([FromBody] UserInteractionRequest request)
        {
            if (request.UserId <= 0 || request.CourseId <= 0)
                return BadRequest("Invalid user ID or course ID");

            if (string.IsNullOrWhiteSpace(request.InteractionType))
                return BadRequest("Interaction type is required");

            try
            {
                var success = await _recommendationService.RecordUserInteractionAsync(
                    request.UserId, request.CourseId, request.InteractionType);

                if (success)
                {
                    return Ok(new { Message = "Interaction recorded successfully" });
                }
                else
                {
                    return BadRequest("Failed to record interaction");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error recording interaction for user {UserId} and course {CourseId}", 
                    request.UserId, request.CourseId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetUserLearningProfile(int userId)
        {
            if (userId <= 0)
                return BadRequest("Invalid user ID");

            try
            {
                var profile = await _recommendationService.GetUserLearningProfileAsync(userId);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting learning profile for user {UserId}", userId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("train-model")]
        public async Task<IActionResult> TrainRecommendationModel()
        {
            try
            {
                await _recommendationService.TrainRecommendationModelAsync();
                return Ok(new { Message = "Model training initiated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initiating model training");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("sync-coursera")]
        public async Task<IActionResult> SyncCourseraCourses()
        {
            try
            {
                var courses = await _recommendationService.SyncCourseraCoursesAsync();
                return Ok(new 
                { 
                    Message = "Coursera courses synced successfully",
                    SyncedCount = courses.Count,
                    SyncedAt = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error syncing Coursera courses");
                return StatusCode(500, "Internal server error");
            }
        }
    }

    // Request models
    public class SkillBasedRecommendationRequest
    {
        public int UserId { get; set; }
        public List<string> TargetSkills { get; set; } = new();
    }

    public class CareerPathRecommendationRequest
    {
        public int UserId { get; set; }
        public string CareerGoal { get; set; } = string.Empty;
    }

    public class UserInteractionRequest
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public string InteractionType { get; set; } = string.Empty; // "view", "enroll", "complete", "rate"
        public string? AdditionalData { get; set; }
    }
}