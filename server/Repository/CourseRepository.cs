using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartCareerPlatform.Data;
using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesBySkillIdsAsync(IEnumerable<int> skillIds)
        {
            return await _context.Courses
                .Where(c => c.Skills.Any(s => skillIds.Contains(int.Parse(s))))
                .ToListAsync();
        }

        public async Task AddCourseAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Course>> GetCoursesByIdsAsync(IEnumerable<int> courseIds)
        {
            return await _context.Courses.Where(c => courseIds.Contains(c.Id)).ToListAsync();
        }
    }
}
