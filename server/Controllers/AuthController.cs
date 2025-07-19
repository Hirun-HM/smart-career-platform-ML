using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartCareerPlatform.Services;
using SmartCareerPlatform.Models;
using Microsoft.AspNetCore.Authorization;

namespace SmartCareerPlatform.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var hashedPassword = HashPassword(loginRequest.Password);
            var user = _authService.GetProfile(loginRequest.Username);

            if (user == null || user.PasswordHash != hashedPassword)
                return Unauthorized();

           
            var jwtKey = "your_super_secret_key_1234567890!@#$"; 
            var jwtIssuer = "your_app";
            var token = (_authService as AuthService)?.GenerateJwtToken(user, jwtKey, jwtIssuer);

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            _logger.LogInformation("Registration attempt for user: {Username}, email: {Email}", registerRequest.Username, registerRequest.Email);

            var hashedPassword = HashPassword(registerRequest.Password);
            _logger.LogInformation("Hashed password: {Hash}", hashedPassword);

            var user = new User
            {
                Username = registerRequest.Username,
                Email = registerRequest.Email,
                PasswordHash = hashedPassword
            };
            var result = _authService.Register(user);

            if (!result)
            {
                _logger.LogWarning("Registration failed for user: {Username}, email: {Email}", registerRequest.Username, registerRequest.Email);
                return BadRequest("Registration failed.");
            }

            _logger.LogInformation("Registration successful for user: {Username}", registerRequest.Username);
            return Ok("Registration successful.");
        }
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var user = _authService.GetProfile(username);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}