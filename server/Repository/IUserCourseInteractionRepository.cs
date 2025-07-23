using System.Collections.Generic;
using System.Threading.Tasks;
using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Repository
{
    public interface IUserCourseInteractionRepository
    {
        Task AddInteractionAsync(UserCourseInteraction interaction);
        Task<IEnumerable<UserCourseInteraction>> GetInteractionsByUserIdAsync(int userId);
        Task<IEnumerable<UserCourseInteraction>> GetInteractionsByCourseIdAsync(int courseId);
    }
}
