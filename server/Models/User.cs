using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartCareerPlatform.Models; 


namespace SmartCareerPlatform.Models;



public class User
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    // public List<string> Skills { get; set; } = new();
    
    public List<string> Interests { get; set; } = new();
    
    public int Experience { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public List<UserCourse> UserCourses { get; set; } = new();
    // public List<UserProgress> UserProgress { get; set; } = new();
    public List<Skill> Skills { get; set; } = new();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}