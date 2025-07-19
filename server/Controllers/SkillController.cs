using Microsoft.AspNetCore.Mvc;
using SmartCareerPlatform.Models;
using SmartCareerPlatform.Services;
using System.Security.Claims;

namespace SmartCareerPlatform.Controllers
{
    [ApiController]
    [Route("api/skills")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkills()
        {
            var skills = await _skillService.GetAllSkillsAsync();
            return Ok(skills);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserSkills(int userId)
        {
            var skills = await _skillService.GetUserSkillsAsync(userId);
            return Ok(skills);
        }

        [HttpPost("user/{userId}")]
        public async Task<IActionResult> AddUserSkill(int userId, [FromBody] AddSkillRequest request)
        {
            var result = await _skillService.AddUserSkillAsync(userId, request.SkillId);
            if (!result)
                return BadRequest("Failed to add skill to user.");
            return Ok("Skill added successfully.");
        }

        [HttpDelete("user/{userId}/skill/{skillId}")]
        public async Task<IActionResult> RemoveUserSkill(int userId, int skillId)
        {
            var result = await _skillService.RemoveUserSkillAsync(userId, skillId);
            if (!result)
                return BadRequest("Failed to remove skill from user.");
            return Ok("Skill removed successfully.");
        }

        [HttpPut("user/{userId}")]
        public async Task<IActionResult> UpdateUserSkills(int userId, [FromBody] UpdateSkillsRequest request)
        {
            var result = await _skillService.UpdateUserSkillsAsync(userId, request.SkillIds);
            if (!result)
                return BadRequest("Failed to update user skills.");
            return Ok("Skills updated successfully.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromBody] Skill skill)
        {
            var createdSkill = await _skillService.CreateSkillAsync(skill);
            return CreatedAtAction(nameof(GetSkillById), new { id = createdSkill.Id }, createdSkill);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkillById(int id)
        {
            var skill = await _skillService.GetSkillByIdAsync(id);
            if (skill == null)
                return NotFound();
            return Ok(skill);
        }
    }

    public class AddSkillRequest
    {
        public int SkillId { get; set; }
    }

    public class UpdateSkillsRequest
    {
        public List<int> SkillIds { get; set; } = new();
    }
}