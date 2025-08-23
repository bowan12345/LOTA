using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model
{
    public class TrimesterCourse
    {
        [Key]
        public string Id { get; set; }

        // FK
        public string CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public string TrimesterId { get; set; }
        [ForeignKey("TrimesterId")]
        public Trimester Trimester { get; set; }

        public string? TutorId { get; set; }
        [ForeignKey("TutorId")]
        public ApplicationUser? Tutor { get; set; }

        // Properties
        public bool? IsActive { get; set; } = true;
        public DateTime? RegistrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
