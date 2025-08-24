using System.ComponentModel.DataAnnotations;

namespace LOTA.Model.DTO.Admin
{
    public class TutorCreateDTO
    {

        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
        [RegularExpression(@"^(?!\d+$)[A-Za-z0-9]+$", ErrorMessage = "FirstName can only contain letters and numbers, and cannot be only numbers")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
        [RegularExpression(@"^(?!\d+$)[A-Za-z0-9]+$", ErrorMessage = "SurName can only contain letters and numbers, and cannot be only numbers")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        //public List<string> AssignedCourses { get; set; } = new List<string>();
    }
}