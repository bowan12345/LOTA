using LOTA.Model;

namespace LOTA.DataAccess.Repository.IRepository
{
    public interface ITrimesterRepository : IRepository<Trimester, string>
    {
        Task<IEnumerable<Trimester>> GetActiveTrimestersAsync();
        Task<Trimester> GetByAcademicYearAndTrimesterAsync(string academicYear, string trimesterNumber);
    }
}
