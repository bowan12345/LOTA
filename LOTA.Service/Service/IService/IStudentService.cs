using LOTA.Model;
using LOTA.Model.DTO.Admin;

namespace LOTA.Service.Service.IService
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentReturnDTO>> GetAllStudentsAsync();
        Task<StudentReturnDTO?> GetStudentByIdAsync(string id);
        Task<IEnumerable<StudentReturnDTO>> GetStudentsByNameOrEmailAsync(string searchTerm);
        Task<StudentReturnDTO> CreateStudentAsync(StudentCreateDTO studentDto);
        Task<StudentReturnDTO> UpdateStudentAsync(StudentUpdateDTO studentDto);
        Task<bool> DeleteStudentAsync(string id);
        Task<(int deletedCount, List<string> errors)> DeleteStudentsAsync(IEnumerable<string> ids);
        Task<bool> IsStudentEmailExistsAsync(string email, string? excludeId = null);
        Task<bool> IsStudentNoExistsAsync(string studentNo, string? excludeId = null);

        /// <summary>
        /// Get enrolled students for a course
        /// </summary>
        /// <param name="courseOfferingId"> offered Course ID</param>
        /// <param name="trimesterId">Trimester id </param>
        /// <returns>List of enrolled students</returns>
        Task<IEnumerable<StudentReturnDTO>> GetEnrolledStudentsAsync(string courseOfferingId, string trimesterId);
    }
}
