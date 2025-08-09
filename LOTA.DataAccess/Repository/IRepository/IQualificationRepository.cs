using LOTA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.DataAccess.Repository.IRepository
{
    public interface IQualificationRepository : IRepository<Qualification, string>
    {
        Task<Qualification?> GetQualificationByIdAsync(string id);
        Task<Qualification?> GetQualificationByNameAsync(string name);
        Task<IEnumerable<Qualification>> GetAllActiveQualificationsAsync();
        Task<IEnumerable<Qualification>> GetAllQualificationsAsync();
        Task<bool> IsQualificationNameExistsAsync(string name, string? excludeId = null);
        Task<IEnumerable<string>> GetAllQualificationTypesAsync();
        Task<QualificationType?> GetQualificationTypeByNameAsync(string name);
    }
}
