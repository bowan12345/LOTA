using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace LOTA.DataAccess.Repository
{
    public class StudentRepository : Repository<ApplicationUser>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext db) : base(db)
        {
        }

        // Batch retrieve student information by IDs
        public async Task<IEnumerable<ApplicationUser>> GetByIdsAsync(List<string> studentIds)
        {
            return await GetAllAsync(s=> studentIds.Contains(s.Id));
        }
    }
}
