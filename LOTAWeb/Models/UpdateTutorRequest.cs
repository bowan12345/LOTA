using System.ComponentModel.DataAnnotations;

namespace LOTAWeb.Models
{
    public class UpdateTutorRequest
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string TutorNo { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public List<string> AssignedCourses { get; set; } = new List<string>();
    }
} 