using System.ComponentModel.DataAnnotations;

namespace LOTA.Model.DTO.Admin
{
    public class UploadStudentsExcelDTO
    {
        [Required]
        public string CourseId { get; set; } = string.Empty;

        [Required]
        public string AcademicYear { get; set; } = string.Empty;

        [Required]
        public string TrimesterNumber { get; set; } = string.Empty;
    }
}
