using System.ComponentModel.DataAnnotations;

namespace LOTA.Model.DTO
{
    public class TutorUpdateDTO
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Password { get; set; }


        public List<string> AssignedCourses { get; set; } = new List<string>();
    }
} 