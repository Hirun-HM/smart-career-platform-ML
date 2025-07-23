using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartCareerPlatform.Data;
using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Repository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Enrollment?> GetEnrollmentByIdAsync(int id)
        {
            return await _context.Enrollments.FindAsync(id);
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByUserIdAsync(int userId)
        {
            return await _context.Enrollments.Where(e => e.UserId == userId).ToListAsync();
        }

        public async Task AddEnrollmentAsync(Enrollment enrollment)
        {
            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEnrollmentAsync(Enrollment enrollment)
        {
            _context.Enrollments.Update(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEnrollmentAsync(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
