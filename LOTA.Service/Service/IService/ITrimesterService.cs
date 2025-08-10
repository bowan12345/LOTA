using LOTA.Model;

namespace LOTA.Service.Service.IService
{
    public interface ITrimesterService
    {
        /// <summary>
        /// Get active trimesters
        /// </summary>
        /// <returns>List of active trimesters</returns>
        Task<IEnumerable<Trimester>> GetActiveTrimestersAsync();
        
        /// <summary>
        /// Get trimester by academic year and trimester number
        /// </summary>
        /// <param name="academicYear">Academic year</param>
        /// <param name="trimesterNumber">Trimester number</param>
        /// <returns>Trimester</returns>
        Task<Trimester> GetByAcademicYearAndTrimesterAsync(int academicYear, int trimesterNumber);

        /// <summary>
        /// Get current trimester based on current date
        /// </summary>
        /// <returns>Current trimester</returns>
        Task<Trimester> GetCurrentTrimesterAsync();
    }
}
