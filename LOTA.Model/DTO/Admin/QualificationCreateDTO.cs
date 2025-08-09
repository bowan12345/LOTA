using System.ComponentModel.DataAnnotations;

namespace LOTA.Model.DTO.Admin
{
    public class QualificationCreateDTO
    {
        [Required(ErrorMessage = "Qualification name is required")]
        [StringLength(50, ErrorMessage = "Qualification name cannot exceed 50 characters")]
        public string QualificationName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Qualification type is required")]
        [StringLength(50, ErrorMessage = "Qualification type cannot exceed 50 characters")]
        public string QualificationType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Level is required")]
        [Range(1,10, ErrorMessage = "Level must be between 1 and 10")]
        public int Level { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
