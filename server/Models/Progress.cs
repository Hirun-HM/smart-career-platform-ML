using System.ComponentModel.DataAnnotations;

namespace SmartCareerPlatform.Models
{
    public class UserProgress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int CompletionPercentage { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public List<string> CompletedModules { get; set; } = new();
        
        
        public User User { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }

    public class SkillProgress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SkillId { get; set; }
        public int ProficiencyLevel { get; set; } // 1-4 (Beginner to Expert)
        public DateTime AcquiredAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public User User { get; set; } = null!;
        public Skill Skill { get; set; } = null!;
    }
}