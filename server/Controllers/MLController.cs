using Microsoft.AspNetCore.Mvc;
using SmartCareerPlatform.Models;
using SmartCareerPlatform.Services;

namespace SmartCareerPlatform.Controllers
{
    [ApiController]
    [Route("api/ml")]
    public class MLController : ControllerBase
    {
        private readonly IMLService _mlService;

        public MLController(IMLService mlService)
        {
            _mlService = mlService;
        }

        [HttpPost("career-predict")]
        public IActionResult PredictCareer([FromBody] CareerPredictionRequest request)
        {
            var result = _mlService.PredictCareer(request);
            return Ok(result);
        }

        [HttpPost("course-recommend")]
        public IActionResult RecommendCourses([FromBody] CourseRecommendationRequest request)
        {
            var result = _mlService.RecommendCourses(request);
            return Ok(result);
        }

        [HttpPost("skill-gap")]
        public IActionResult AnalyzeSkillGap([FromBody] SkillGapAnalysisRequest request)
        {
            var result = _mlService.AnalyzeSkillGap(request);
            return Ok(result);
        }
    }
}