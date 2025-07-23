using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartCareerPlatform.Data;
using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Repository
{
    public class UserCourseInteractionRepository : IUserCourseInteractionRepository
    {
        private readonly ApplicationDbContext _context;
        public UserCourseInteractionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddInteractionAsync(UserCourseInteraction interaction)
        {
            await _context.UserCourseInteractions.AddAsync(interaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserCourseInteraction>> GetInteractionsByUserIdAsync(int userId)
        {
            return await _context.UserCourseInteractions.Where(i => i.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<UserCourseInteraction>> GetInteractionsByCourseIdAsync(int courseId)
        {
            return await _context.UserCourseInteractions.Where(i => i.CourseId == courseId).ToListAsync();
        }
    }
}
