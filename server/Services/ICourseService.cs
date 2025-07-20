using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Services
{
    public interface ICourseService
    {
        Task<List<CourseRecommendationResponse>> GetRecommendedCoursesAsync(int userId);
        Task<bool> EnrollCourseAsync(int courseId);
        Task<List<Course>> GetAllCoursesAsync();
        Task<Course?> GetCourseByIdAsync(int courseId);
    }
}