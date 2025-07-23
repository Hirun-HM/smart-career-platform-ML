using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartCareerPlatform.Models;
using SmartCareerPlatform.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SmartCareerPlatform.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string? Authenticate(User user)
        {
            // For demo: use repository to get user by email or username
            var found = _userRepository.GetUserByEmailAsync(user.Email).Result;
            return found != null && found.PasswordHash == user.PasswordHash ? "mock-token" : null;
        }

        public bool Register(User user)
        {
            var existing = _userRepository.GetUserByEmailAsync(user.Email).Result;
            if (existing != null)
                return false;
            _userRepository.AddUserAsync(user).Wait();
            return true;
        }

        public User? GetProfile(string? username)
        {
            // For demo: not async, but should be
            return _userRepository.GetAllUsersAsync().Result.FirstOrDefault(u => u.Username == username);
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public bool UpdateProfile(User user)
        {
            var existing = _userRepository.GetUserByIdAsync(user.Id).Result;
            if (existing == null) return false;
            existing.Email = user.Email;
            existing.Experience = user.Experience;
            existing.Username = user.Username;
            _userRepository.UpdateUserAsync(existing).Wait();
            return true;
        }

        public string GenerateJwtToken(User user, string jwtKey, string jwtIssuer)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}