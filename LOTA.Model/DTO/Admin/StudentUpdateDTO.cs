using System.ComponentModel.DataAnnotations;

namespace LOTA.Model.DTO.Admin
{
    public class StudentUpdateDTO
    {
        [Required(ErrorMessage = "Student ID is required")]
        public string Id { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
        [RegularExpression(@"^(?!\d+$)[A-Za-z0-9]+$", ErrorMessage = "FirstName can only contain letters and numbers, and cannot be only numbers")]
        public string? FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
        [RegularExpression(@"^(?!\d+$)[A-Za-z0-9]+$", ErrorMessage = "SurName can only contain letters and numbers, and cannot be only numbers")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Student No cannot exceed 50 characters")]
        public string? StudentNo { get; set; }

        [StringLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
        public string? Password { get; set; }

        [StringLength(100, ErrorMessage = "Confirm Password cannot exceed 100 characters")]
        public string? ConfirmPassword { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
