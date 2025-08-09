using LOTA.DataAccess.Data;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using Microsoft.EntityFrameworkCore;

namespace LOTA.DataAccess.Repository
{
    public class QualificationRepository : Repository<Qualification>, IQualificationRepository
    {

        public QualificationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Qualification?> GetQualificationByIdAsync(string id)
        {
            return await _db.Qualification
                .Include(q => q.Courses)
                .Include(q => q.QualificationType)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Qualification?> GetQualificationByNameAsync(string name)
        {
            return await _db.Qualification
                .Include(q => q.Courses)
                .FirstOrDefaultAsync(q => q.QualificationName == name);
        }

        public async Task<IEnumerable<Qualification>> GetAllActiveQualificationsAsync()
        {
            return await _db.Qualification
                .Include(q => q.Courses)
                .Include(q => q.QualificationType)
                .Where(q => q.IsActive)
                .OrderBy(q => q.QualificationName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Qualification>> GetAllQualificationsAsync()
        {
            return await _db.Qualification
                .Include(q => q.Courses)
                .Include(q => q.QualificationType)
                .OrderBy(q => q.QualificationName)
                .ToListAsync();
        }

        public async Task<bool> IsQualificationNameExistsAsync(string name, string? excludeId = null)
        {
            if (string.IsNullOrEmpty(excludeId))
            {
                return await _db.Qualification
                    .AnyAsync(q => q.QualificationName == name);
            }

            return await _db.Qualification
                .AnyAsync(q => q.QualificationName == name && q.Id != excludeId);
        }

        public async Task<IEnumerable<string>> GetAllQualificationTypesAsync()
        {
            return await _db.QualificationType
                .Select(qt => qt.QualificationTypeName)
                .OrderBy(name => name)
                .ToListAsync();
        }

        public async Task<QualificationType?> GetQualificationTypeByTypeNameAsync(string name)
        {
            return await _db.QualificationType
                .FirstOrDefaultAsync(qt => qt.QualificationTypeName == name);
        }
    }
}
