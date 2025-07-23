using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartCareerPlatform.Data;
using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly ApplicationDbContext _context;
        public SkillRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Skill?> GetSkillByIdAsync(int id)
        {
            return await _context.Skills.FindAsync(id);
        }

        public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task<IEnumerable<Skill>> GetSkillsByUserIdAsync(int userId)
        {
            // This would need to be implemented based on your User-Skill relationship
            // For now, return empty list as Skills don't have a Users navigation property
            return await Task.FromResult(new List<Skill>());
        }

        public async Task AddSkillAsync(Skill skill)
        {
            await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSkillAsync(Skill skill)
        {
            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSkillAsync(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill != null)
            {
                _context.Skills.Remove(skill);
                await _context.SaveChangesAsync();
            }
        }
    }
}
