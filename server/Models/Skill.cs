using System.ComponentModel.DataAnnotations;

namespace SmartCareerPlatform.Models;

public class Skill
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    public string Category { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public List<string> RelatedSkills { get; set; } = new();
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}