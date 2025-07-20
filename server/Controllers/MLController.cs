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
        private readonly IAuthService _authService;

        public MLController(IMLService mlService, IAuthService authService)
        {
            _mlService = mlService;
            _authService = authService;
        }

        [HttpPost("predict-career")]
        public async Task<IActionResult> PredictCareer([FromBody] CareerPredictionRequest request)
        {
            var result = await _mlService.PredictCareerAsync(request);
            return Ok(result);
        }

        [HttpPost("predict-career/user/{userId}")]
        public async Task<IActionResult> PredictCareerForUser(int userId)
        {
            var user = await _authService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();

            var request = new CareerPredictionRequest
            {
                Skills = user.Skills.Select(s => s.Name).ToList(),
                Interests = user.Interests,
                Experience = user.Experience
            };

            var result = await _mlService.PredictCareerAsync(request);
            return Ok(result);
        }
    }
}