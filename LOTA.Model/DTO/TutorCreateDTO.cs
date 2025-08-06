using System.ComponentModel.DataAnnotations;

namespace LOTA.Model.DTO
{
    public class TutorCreateDTO
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        public List<string> AssignedCourses { get; set; } = new List<string>();
    }
} 