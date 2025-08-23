using System;

namespace LOTA.Model.DTO.Admin
{
    public class TrimesterCourseReturnDTO
    {
        public string Id { get; set; }
        public string TrimesterId { get; set; }
        public string CourseId { get; set; }
        public string? TutorId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // Navigation properties
        public TrimesterReturnDTO Trimester { get; set; }
        public CourseReturnDTO Course { get; set; }
        public UserReturnDTO Tutor { get; set; }
    }

    public class UserReturnDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
