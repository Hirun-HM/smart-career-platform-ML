
namespace SmartCareerPlatform.Models;

public class Enrollment
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int UserId { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public string Status { get; set; } = "Active";

  
    public Course Course { get; set; }
    public User User { get; set; }
}