using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Services
{
    public interface ISkillService
    {
        Task<List<Skill>> GetAllSkillsAsync();
        Task<List<Skill>> GetUserSkillsAsync(int userId);
        Task<bool> AddUserSkillAsync(int userId, int skillId);
        Task<bool> RemoveUserSkillAsync(int userId, int skillId);
        Task<bool> UpdateUserSkillsAsync(int userId, List<int> skillIds);
        Task<Skill?> GetSkillByIdAsync(int skillId);
        Task<Skill> CreateSkillAsync(Skill skill);
    }
}