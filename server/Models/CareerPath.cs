using System.ComponentModel.DataAnnotations;

namespace SmartCareerPlatform.Models;

public class CareerPath
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    public List<string> RequiredSkills { get; set; } = new();
    
    public decimal AverageSalary { get; set; }
    
    public decimal GrowthRate { get; set; }
    
    public string Industry { get; set; } = string.Empty;
    
    public string ExperienceLevel { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}