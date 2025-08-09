using System.ComponentModel.DataAnnotations;

namespace LOTA.Model.DTO.Admin
{
    public class StudentUpdateDTO
    {
        [Required(ErrorMessage = "Student ID is required")]
        public string Id { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
        public string? FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Student No cannot exceed 255 characters")]
        public string? StudentNo { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
