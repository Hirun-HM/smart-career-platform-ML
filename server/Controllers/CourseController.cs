using Microsoft.AspNetCore.Mvc;
using SmartCareerPlatform.Models;
using SmartCareerPlatform.Services;

namespace SmartCareerPlatform.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("recommend")]
        public IActionResult GetRecommendations([FromQuery] int userId)
        {
            var courses = _courseService.GetRecommendedCourses(userId);
            return Ok(courses);
        }

        [HttpPost("enroll")]
        public IActionResult EnrollCourse([FromBody] int courseId)
        {
            var result = _courseService.EnrollCourse(courseId);
            if (!result)
                return BadRequest("Enrollment failed.");
            return Ok("Enrolled successfully.");
        }
    }
}