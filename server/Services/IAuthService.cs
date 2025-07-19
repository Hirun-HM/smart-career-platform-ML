using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Services
{
    public interface IAuthService
    {
        string? Authenticate(User user);
        bool Register(User user);
        User? GetProfile(string? username);
        bool UpdateProfile(User user);
    }
}