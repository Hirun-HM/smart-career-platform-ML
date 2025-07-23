using Microsoft.EntityFrameworkCore;
using SmartCareerPlatform.Models;
using SmartCareerPlatform.Repository;

namespace SmartCareerPlatform.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IUserRepository _userRepository;

        public SkillService(ISkillRepository skillRepository, IUserRepository userRepository)
        {
            _skillRepository = skillRepository;
            _userRepository = userRepository;
        }

        public async Task<List<Skill>> GetAllSkillsAsync()
        {
            return (await _skillRepository.GetAllSkillsAsync()).ToList();
        }

        public async Task<List<Skill>> GetUserSkillsAsync(int userId)
        {
            var skills = await _skillRepository.GetSkillsByUserIdAsync(userId);
            return skills.ToList();
        }

        public async Task<bool> AddUserSkillAsync(int userId, int skillId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            var skill = await _skillRepository.GetSkillByIdAsync(skillId);
            if (user == null || skill == null)
                return false;
            if (user.Skills.Any(s => s.Id == skillId))
                return false;
            user.Skills.Add(skill);
            await _userRepository.UpdateUserAsync(user);
            return true;
        }

        public async Task<bool> RemoveUserSkillAsync(int userId, int skillId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                return false;
            var skill = user.Skills.FirstOrDefault(s => s.Id == skillId);
            if (skill == null)
                return false;
            user.Skills.Remove(skill);
            await _userRepository.UpdateUserAsync(user);
            return true;
        }

        public async Task<bool> UpdateUserSkillsAsync(int userId, List<int> skillIds)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                return false;
            var skills = (await _skillRepository.GetAllSkillsAsync()).Where(s => skillIds.Contains(s.Id)).ToList();
            user.Skills.Clear();
            user.Skills.AddRange(skills);
            await _userRepository.UpdateUserAsync(user);
            return true;
        }

        public async Task<Skill?> GetSkillByIdAsync(int skillId)
        {
            return await _skillRepository.GetSkillByIdAsync(skillId);
        }

        public async Task<Skill> CreateSkillAsync(Skill skill)
        {
            await _skillRepository.AddSkillAsync(skill);
            return skill;
        }
    }
}