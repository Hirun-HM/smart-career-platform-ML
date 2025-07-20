using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Services
{
    public interface IAuthService
    {
        string? Authenticate(User user);
        bool Register(User user);
        User? GetProfile(string? username);
        bool UpdateProfile(User user);
        Task<User?> GetUserByIdAsync(int userId); 
        string GenerateJwtToken(User user, string jwtKey, string jwtIssuer);
    }
}