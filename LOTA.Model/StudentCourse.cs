using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model
{
    public class StudentCourse
    {
        [Key]
        public string Id { get; set; }

        // FKs
        public string StudentNo { get; set; }
        [ForeignKey("StudentNo")]
        public ApplicationUser Student { get; set; }

        public string CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        // Properties
        public bool? IsActive { get; set; } = true;
        public DateTime? RegistrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
