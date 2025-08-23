using System.ComponentModel.DataAnnotations;

namespace LOTA.Model.DTO.Admin
{
    public class AddStudentsToCourseOfferingDTO
    {
        [Required]
        public string CourseOfferingId { get; set; } = string.Empty;

        [Required]
        public List<string> StudentIds { get; set; } = new List<string>();

        [Required]
        public int AcademicYear { get; set; } 

        [Required]
        public int TrimesterNumber { get; set; }

        [Required]
        public string TrimesterId { get; set; } = string.Empty;
    }
}
