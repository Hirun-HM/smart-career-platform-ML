using System.ComponentModel.DataAnnotations;

namespace SmartCareerPlatform.Models;

public class Course
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    public List<string> Skills { get; set; } = new();
    
    public int Duration { get; set; } 
    
    public decimal Rating { get; set; } = 0;
    
    public string Url { get; set; } = string.Empty;
    
    public string Category { get; set; } = string.Empty;
    
    public string Level { get; set; } = string.Empty;
    
    // Add these properties for external courses
    public string Instructor { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public string Provider { get; set; } = "Internal"; // "Internal", "Coursera", etc.
    public string ExternalId { get; set; } = string.Empty; // For Coursera course ID
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public List<UserCourse> UserCourses { get; set; } = new();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}

public class UserCourse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    public int Progress { get; set; } = 0; 
    
    // Navigation properties
    public User User { get; set; } = null!;
    public Course Course { get; set; } = null!;
}