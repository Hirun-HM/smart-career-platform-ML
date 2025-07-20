using Microsoft.AspNetCore.Mvc;
using SmartCareerPlatform.Models;
using SmartCareerPlatform.Services;

namespace SmartCareerPlatform.Controllers
{
    [ApiController]
    [Route("api/learning-path")]
    public class LearningPathController : ControllerBase
    {
        private readonly IMLService _mlService;
        private readonly ICourseService _courseService;

        public LearningPathController(IMLService mlService, ICourseService courseService)
        {
            _mlService = mlService;
            _courseService = courseService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetLearningPath(int userId)
        {
            var courses = await _courseService.GetRecommendedCoursesAsync(userId);
            return Ok(courses);
        }

        [HttpPost("generate/{userId}")]
        public async Task<IActionResult> GenerateLearningPath(int userId, [FromBody] LearningPathRequest request)
        {
            var path = await _mlService.GenerateLearningPathAsync(userId, request.TargetCareer);
            return Ok(path);
        }
    }
}