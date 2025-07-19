using SmartCareerPlatform.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartCareerPlatform.Services
{
    public class CourseService
    {
        // In-memory course store for demonstration
        private static List<CourseRecommendationResponse> _courses = new List<CourseRecommendationResponse>
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

        private static Dictionary<int, List<int>> _enrollments = new Dictionary<int, List<int>>();

        public List<CourseRecommendationResponse> GetRecommendedCourses(int userId)
        {
            // Simple logic: recommend all courses not yet enrolled by user
            var enrolled = _enrollments.ContainsKey(userId) ? _enrollments[userId] : new List<int>();
            return _courses.Where(c => !enrolled.Contains(c.Id)).ToList();
        }

        public bool EnrollCourse(int courseId)
        {
            // For demo, assume userId = 1
            int userId = 1;
            if (!_courses.Any(c => c.Id == courseId))
                return false;
            if (!_enrollments.ContainsKey(userId))
                _enrollments[userId] = new List<int>();
            if (_enrollments[userId].Contains(courseId))
                return false;
            _enrollments[userId].Add(courseId);
            return true;
        }
    }
}