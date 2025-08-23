using System.ComponentModel.DataAnnotations;

namespace LOTA.Model.DTO.Admin
{
    public class TrimesterCourseCreateDTO
    {
        [Required(ErrorMessage = "Trimester ID is required")]
        public string TrimesterId { get; set; }

        [Required(ErrorMessage = "Course ID is required")]
        public string CourseId { get; set; }

        public string? TutorId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
