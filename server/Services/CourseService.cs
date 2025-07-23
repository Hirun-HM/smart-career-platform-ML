using Microsoft.EntityFrameworkCore;
using SmartCareerPlatform.Models;
using SmartCareerPlatform.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCareerPlatform.Services
{
    public class CourseService : ICourseService
    {
        private const string InternalCourseBaseUrl = "https://internal.example.com";

        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public CourseService(ICourseRepository courseRepository, IEnrollmentRepository enrollmentRepository)
        {
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
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
                    Url = $"{InternalCourseBaseUrl}/ai-course",
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
                    Url = $"{InternalCourseBaseUrl}/web-course",
                    RecommendationScore = 0.90m
                }
            };
        }

        public async Task<bool> EnrollCourseAsync(int courseId)
        {
            try
            {
                // Validate the course exists
                var course = await _courseRepository.GetCourseByIdAsync(courseId);
                if (course == null)
                    return false;

                // Get current user ID from authentication context (placeholder implementation)
                var userId = 1; // Replace with actual user ID from claims/context

                // Check if user is already enrolled
                var userEnrollments = await _enrollmentRepository.GetEnrollmentsByUserIdAsync(userId);
                var existingEnrollment = userEnrollments.FirstOrDefault(e => e.CourseId == courseId);
                
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

                await _enrollmentRepository.AddEnrollmentAsync(enrollment);
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return (await _courseRepository.GetAllCoursesAsync()).ToList();
        }

        public async Task<Course?> GetCourseByIdAsync(int courseId)
        {
            return await _courseRepository.GetCourseByIdAsync(courseId);
        }
    }
}