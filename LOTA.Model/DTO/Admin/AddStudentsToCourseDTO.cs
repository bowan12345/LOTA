using System.ComponentModel.DataAnnotations;

namespace LOTA.Model.DTO.Admin
{
    public class AddStudentsToCourseDTO
    {
        [Required]
        public string CourseId { get; set; } = string.Empty;

        [Required]
        public List<string> StudentIds { get; set; } = new List<string>();
    }
}
