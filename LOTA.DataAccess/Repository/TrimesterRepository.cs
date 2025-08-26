using LOTA.DataAccess.Data;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using Microsoft.EntityFrameworkCore;

namespace LOTA.DataAccess.Repository
{
    public class TrimesterRepository : Repository<Trimester>, ITrimesterRepository
    {
        public TrimesterRepository(ApplicationDbContext db) : base(db) { }

        public async Task<IEnumerable<Trimester>> GetActiveTrimestersAsync()
        {
            return await _db.Trimester
                .Where(t => t.IsActive)
                .OrderByDescending(t => t.AcademicYear)
                .ThenByDescending(t => t.TrimesterNumber)
                .ToListAsync();
        }

        public async Task<Trimester> GetByAcademicYearAndTrimesterAsync(int academicYear, int trimesterNumber)
        {
            return await _db.Trimester
                .FirstOrDefaultAsync(t => t.AcademicYear == academicYear && t.TrimesterNumber == trimesterNumber);
        }

        public async Task<IEnumerable<Trimester>> GetByAcademicYearAsync(int academicYear)
        {
            return await _db.Trimester.Where(t => t.AcademicYear == academicYear).ToListAsync();
        }

        public async Task<Trimester> GetLatestTrimestersAsync()
        {
            return await _db.Trimester
               .Where(t => t.IsActive)
               .OrderByDescending(t => t.AcademicYear)
               .ThenByDescending(t => t.TrimesterNumber)
               .FirstOrDefaultAsync();
        }
    }
}
