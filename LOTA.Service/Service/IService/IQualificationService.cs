using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOTA.Model.DTO.Admin;

namespace LOTA.Service.Service.IService
{
    public interface IQualificationService
    {
        /// <summary>
        /// Get all active qualifications
        /// </summary>
        /// <returns>List of qualifications</returns>
        Task<IEnumerable<QualificationReturnDTO>> GetAllQualificationsAsync();

        /// <summary>
        /// Get qualification by ID
        /// </summary>
        /// <param name="id">Qualification ID</param>
        /// <returns>Qualification details</returns>
        Task<QualificationReturnDTO?> GetQualificationByIdAsync(string id);

        /// <summary>
        /// Create a new qualification
        /// </summary>
        /// <param name="qualification">Qualification data</param>
        /// <returns>Created qualification</returns>
        Task<QualificationReturnDTO> CreateQualificationAsync(QualificationCreateDTO qualification);

        /// <summary>
        /// Update an existing qualification
        /// </summary>
        /// <param name="qualification">Updated qualification data</param>
        /// <returns>Updated qualification</returns>
        Task<QualificationReturnDTO> UpdateQualificationAsync(QualificationUpdateDTO qualification);

        /// <summary>
        /// Delete a qualification
        /// </summary>
        /// <param name="id">Qualification ID</param>
        /// <returns>Success status</returns>
        Task<bool> DeleteQualificationAsync(string id);

        /// <summary>
        /// Check if qualification name exists
        /// </summary>
        /// <param name="name">Qualification name</param>
        /// <param name="excludeId">ID to exclude from check</param>
        /// <returns>True if exists</returns>
        Task<bool> IsQualificationNameExistsAsync(string name, string? excludeId = null);

        /// <summary>
        /// Get all unique qualification types from database
        /// </summary>
        /// <returns>List of unique qualification types</returns>
        Task<IEnumerable<string>> GetAllQualificationTypesAsync();
    }
}
