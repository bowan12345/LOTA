using LOTA.Model;

namespace LOTA.DataAccess.Repository.IRepository
{
    public interface ITrimesterRepository : IRepository<Trimester, string>
    {
        Task<IEnumerable<Trimester>> GetActiveTrimestersAsync();

        Task<Trimester> GetLatestTrimestersAsync();
        Task<IEnumerable<Trimester>> GetByAcademicYearAsync(int academicYear);
        Task<Trimester> GetByAcademicYearAndTrimesterAsync(int academicYear, int trimesterNumber);
    }
}
