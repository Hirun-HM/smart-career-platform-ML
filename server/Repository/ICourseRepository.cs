using System.Collections.Generic;
using System.Threading.Tasks;
using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Repository
{
    public interface ICourseRepository
    {
        Task<Course?> GetCourseByIdAsync(int id);
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<IEnumerable<Course>> GetCoursesBySkillIdsAsync(IEnumerable<int> skillIds);
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
        Task<IEnumerable<Course>> GetCoursesByIdsAsync(IEnumerable<int> courseIds);
    }
}
