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

        
    }
}