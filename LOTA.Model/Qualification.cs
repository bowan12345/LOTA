using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model
{
    public class Qualification
    {
        [Key]
        public string Id { get; set; }

        //Eg:Bachelor of Information Technology
        [Required, MaxLength(50)]
        public string QualificationName { get; set; }

        //Eg: Bachelor, Diploma
        [Required, MaxLength(50)]
        public string QualificationType { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // Relationships
        public ICollection<Course> Courses { get; set; }
    }
}
