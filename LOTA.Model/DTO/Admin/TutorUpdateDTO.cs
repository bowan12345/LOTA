using System.ComponentModel.DataAnnotations;

namespace LOTA.Model.DTO.Admin
{
    public class TutorUpdateDTO
    {
        [Required]
        public string Id { get; set; }

        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
        [RegularExpression(@"^(?!\d+$).*$", ErrorMessage = "FirstName cannot be only numbers")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
        [RegularExpression(@"^(?!\d+$).*$", ErrorMessage = "SurName cannot be only numbers")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        //public List<string> AssignedCourses { get; set; } = new List<string>();
    }
}