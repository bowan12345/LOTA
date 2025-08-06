using System.ComponentModel.DataAnnotations;

namespace LOTAWeb.Models
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
        [MinLength(6)]
        public string Password { get; set; }

        public List<string> AssignedCourses { get; set; } = new List<string>();
    }
} 