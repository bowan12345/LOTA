using LOTA.Model;

namespace LOTA.DataAccess.Repository.IRepository
{
    public interface IStudentRepository : IRepository<ApplicationUser, string>
    {
        Task<IEnumerable<ApplicationUser>> GetByIdsAsync(List<string> studentIds);
    }
}
