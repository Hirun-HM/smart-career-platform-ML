using System.Collections.Generic;
using System.Threading.Tasks;
using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Repository
{
    public interface ISkillRepository
    {
        Task<Skill?> GetSkillByIdAsync(int id);
        Task<IEnumerable<Skill>> GetAllSkillsAsync();
        Task<IEnumerable<Skill>> GetSkillsByUserIdAsync(int userId);
        Task AddSkillAsync(Skill skill);
        Task UpdateSkillAsync(Skill skill);
        Task DeleteSkillAsync(int id);
    }
}
