using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model
{
    public class TutorCourse
    {

        [Key]
        public string? Id { get; set; }
        public string? TutorId { get; set; }
        [ForeignKey("TutorId")]
        public ApplicationUser? Tutor { get; set; }

        public string? CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course? Course { get; set; }
    }
}
