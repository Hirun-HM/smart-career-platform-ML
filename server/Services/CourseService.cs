using Microsoft.EntityFrameworkCore;
using SmartCareerPlatform.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCareerPlatform.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CourseRecommendationResponse>> GetRecommendedCoursesAsync(int userId)
        {
            // Mock implementation - replace with actual ML recommendation logic
            await Task.Delay(100);

            return new List<CourseRecommendationResponse>
            {
                new CourseRecommendationResponse
                {
                    Id = 1,
                    Title = "Introduction to AI",
                    Description = "Learn the basics of Artificial Intelligence.",
                    Skills = new List<string> { "AI", "Machine Learning" },
                    Duration = 10,
                    Rating = 4.5m,
                    Url = "https://example.com/ai-course",
                    RecommendationScore = 0.95m
                },
                new CourseRecommendationResponse
                {
                    Id = 2,
                    Title = "Web Development Bootcamp",
                    Description = "Become a full-stack web developer.",
                    Skills = new List<string> { "HTML", "CSS", "JavaScript" },
                    Duration = 20,
                    Rating = 4.7m,
                    Url = "https://example.com/web-course",
                    RecommendationScore = 0.90m
                }
            };
        }

       

        public async Task<bool> EnrollCourseAsync(int courseId)
        {
            try
            {
                // Validate the course exists
                var course = await _context.Courses.FindAsync(courseId);
                if (course == null)
                    return false;

                // TODO: Get current user ID from authentication context
                // For now, using a placeholder
                var userId = 1; // Replace with actual user ID from claims/context

                // Check if user is already enrolled
                var existingEnrollment = await _context.Enrollments
                    .FirstOrDefaultAsync(e => e.CourseId == courseId && e.UserId == userId);
                
                if (existingEnrollment != null)
                    return false; // Already enrolled

                // Create new enrollment
                var enrollment = new Enrollment
                {
                    CourseId = courseId,
                    UserId = userId,
                    EnrollmentDate = DateTime.UtcNow,
                    Status = "Active"
                };

                _context.Enrollments.Add(enrollment);
                await _context.SaveChangesAsync();
                
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception in a real implementation
                // _logger.LogError(ex, "Failed to enroll in course {CourseId}", courseId);
                return false;
            }
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int courseId)
        {
            return await _context.Courses.FindAsync(courseId);
        }
    }
}