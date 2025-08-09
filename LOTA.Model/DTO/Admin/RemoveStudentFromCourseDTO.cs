using System.ComponentModel.DataAnnotations;

namespace LOTA.Model.DTO.Admin
{
    public class RemoveStudentFromCourseDTO
    {
        [Required]
        public string CourseId { get; set; } = string.Empty;

        [Required]
        public string StudentId { get; set; } = string.Empty;
    }
}
