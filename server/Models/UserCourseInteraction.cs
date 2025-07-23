using System;

namespace SmartCareerPlatform.Models
{
    public class UserCourseInteraction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public string InteractionType { get; set; } = string.Empty; // "view", "enroll", "complete", "rate"
        public DateTime Timestamp { get; set; }
        public string? AdditionalData { get; set; } // JSON for extra data like rating value
        
        public User User { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}
