using Microsoft.EntityFrameworkCore;
using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Services
{
    public class SkillService : ISkillService
    {
        private readonly ApplicationDbContext _context;

        public SkillService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Skill>> GetAllSkillsAsync()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task<List<Skill>> GetUserSkillsAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Skills)
                .FirstOrDefaultAsync(u => u.Id == userId);
            
            return user?.Skills ?? new List<Skill>();
        }

        public async Task<bool> AddUserSkillAsync(int userId, int skillId)
        {
            var user = await _context.Users
                .Include(u => u.Skills)
                .FirstOrDefaultAsync(u => u.Id == userId);
            
            var skill = await _context.Skills.FindAsync(skillId);
            
            if (user == null || skill == null)
                return false;
            
            if (user.Skills.Any(s => s.Id == skillId))
                return false;
            
            user.Skills.Add(skill);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveUserSkillAsync(int userId, int skillId)
        {
            var user = await _context.Users
                .Include(u => u.Skills)
                .FirstOrDefaultAsync(u => u.Id == userId);
            
            if (user == null)
                return false;
            
            var skill = user.Skills.FirstOrDefault(s => s.Id == skillId);
            if (skill == null)
                return false;
            
            user.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserSkillsAsync(int userId, List<int> skillIds)
        {
            var user = await _context.Users
                .Include(u => u.Skills)
                .FirstOrDefaultAsync(u => u.Id == userId);
            
            if (user == null)
                return false;
            
            var skills = await _context.Skills
                .Where(s => skillIds.Contains(s.Id))
                .ToListAsync();
            
            user.Skills.Clear();
            user.Skills.AddRange(skills);
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Skill?> GetSkillByIdAsync(int skillId)
        {
            return await _context.Skills.FindAsync(skillId);
        }

        public async Task<Skill> CreateSkillAsync(Skill skill)
        {
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            return skill;
        }
    }
}