using System.Collections.Generic;
using System.Threading.Tasks;
using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Repository
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment?> GetEnrollmentByIdAsync(int id);
        Task<IEnumerable<Enrollment>> GetEnrollmentsByUserIdAsync(int userId);
        Task AddEnrollmentAsync(Enrollment enrollment);
        Task UpdateEnrollmentAsync(Enrollment enrollment);
        Task DeleteEnrollmentAsync(int id);
    }
}
