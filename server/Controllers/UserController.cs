using Microsoft.AspNetCore.Mvc;
using SmartCareerPlatform.Models;
using SmartCareerPlatform.Services;

namespace SmartCareerPlatform.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPut("update")]
        public IActionResult UpdateProfile([FromBody] User user)
        {
            var result = _authService.UpdateProfile(user);
            if (!result)
                return BadRequest("Update failed.");
            return Ok("Profile updated successfully.");
        }

        [HttpGet("progress")]
        public IActionResult GetProgress()
        {
            // Mock progress data
            var progress = new
            {
                totalCourses = 12,
                completedCourses = 7,
                skillsAcquired = 15,
                weeklyProgress = 15,
                monthlyProgress = 45,
                recentAchievements = new[]
                {
                    "Completed React Fundamentals course",
                    "Acquired TypeScript skill",
                    "Reached 80% in Web Development path"
                }
            };

            return Ok(progress);
        }

        [HttpPut("progress")]
        public async Task<IActionResult> UpdateProgress([FromBody] UpdateProgressRequest request)
        {
            // Mock implementation
            await Task.Delay(100);
            return Ok("Progress updated successfully.");
        }
    }

    public class UpdateProgressRequest
    {
        public int CourseId { get; set; }
        public int Percentage { get; set; }
    }
}