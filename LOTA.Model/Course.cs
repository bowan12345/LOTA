using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LOTA.Model
{
    public class Course
    {
        [Key]
        public string Id { get; set; }

        [Required, MaxLength(100)]
        public string CourseName { get; set; }

        [Required, MaxLength(50)]
        public string CourseCode { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // FK
        public string? QualificationId { get; set; }
        [ForeignKey("QualificationId")]
        public Qualification? Qualification { get; set; }

        // Relationships
        public ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();
        public ICollection<LearningOutcome> LearningOutcomes { get; set; } = new List<LearningOutcome>();
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    }
}
