using LOTA.Model;
using LOTA.Model.DTO.Admin;

namespace LOTA.Service.Service.IService
{
    public interface ITrimesterService
    {
        /// <summary>
        /// Get active trimesters
        /// </summary>
        /// <returns>List of active trimesters</returns>
        Task<IEnumerable<TrimesterReturnDTO>> GetActiveTrimestersAsync();
        
        /// <summary>
        /// Get trimester by academic year and trimester number
        /// </summary>
        /// <param name="academicYear">Academic year</param>
        /// <param name="trimesterNumber">Trimester number</param>
        /// <returns>Trimester</returns>
        Task<TrimesterReturnDTO> GetByAcademicYearAndTrimesterAsync(int academicYear, int trimesterNumber);

        /// <summary>
        /// Get current trimester based on current date
        /// </summary>
        /// <returns>Current trimester</returns>
        Task<TrimesterReturnDTO> GetCurrentTrimesterAsync();

        /// <summary>
        /// Get trimester by ID
        /// </summary>
        /// <param name="id">Trimester ID</param>
        /// <returns>Trimester</returns>
        Task<TrimesterReturnDTO> GetByIdAsync(string id);

        /// <summary>
        /// Create new trimester
        /// </summary>
        /// <param name="trimester">Trimester to create</param>
        /// <returns>Success status</returns>
        Task<TrimesterReturnDTO> CreateAsync(TrimesterCreateDTO trimester);

        /// <summary>
        /// Update existing trimester
        /// </summary>
        /// <param name="trimester">Trimester to update</param>
        /// <returns>Success status</returns>
        Task<TrimesterReturnDTO> UpdateAsync(TrimesterUpdateDTO trimester);

        /// <summary>
        /// Delete trimester
        /// </summary>
        /// <param name="id">Trimester ID</param>
        /// <returns>Success status</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// Delete add trimester
        /// </summary>
        /// <param name="ids"> a list of Trimester ID</param>
        /// <returns></returns>
        Task DeleteAllAsync(IEnumerable<string> ids);

        /// <summary>
        /// get trimesters by academic year
        /// </summary>
        /// <param name="academicYear"> Academic year</param>
        /// <returns>   a list of trimesters</returns>
        Task<IEnumerable<TrimesterReturnDTO>> GetByAcademicYearAsync(int academicYear);
    }
}
