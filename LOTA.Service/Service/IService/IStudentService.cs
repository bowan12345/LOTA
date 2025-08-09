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
        Task<bool> IsStudentEmailExistsAsync(string email, string? excludeId = null);
        Task<bool> IsStudentNoExistsAsync(string studentNo, string? excludeId = null);
    }
}
