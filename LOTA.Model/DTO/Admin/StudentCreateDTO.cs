using System.ComponentModel.DataAnnotations;

namespace LOTA.Model.DTO.Admin
{
    public class StudentCreateDTO
    {
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
        public string? FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Student No cannot exceed 50 characters")]
        public string? StudentNo { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
