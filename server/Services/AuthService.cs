using Microsoft.IdentityModel.Tokens;
using SmartCareerPlatform.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SmartCareerPlatform.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string? Authenticate(User user)
        {
            var found = _context.Users.FirstOrDefault(u => u.Username == user.Username && u.PasswordHash == user.PasswordHash);
            return found != null ? "mock-token" : null;
        }

        public bool Register(User user)
        {
            if (_context.Users.Any(u => u.Username == user.Username || u.Email == user.Email))
                return false;
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public User? GetProfile(string? username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public bool UpdateProfile(User user)
        {
            var existing = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (existing == null) return false;
            existing.Email = user.Email;
            existing.Experience = user.Experience;
            existing.Username = user.Username;
            _context.SaveChanges();
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