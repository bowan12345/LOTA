using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string QualificationTypeId { get; set; }
        [ForeignKey("QualificationTypeId")]
        public QualificationType QualificationType { get; set; }

        [Required]
        public int Level { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // Relationships
        public ICollection<Course> Courses { get; set; }
    }


    public class QualificationType 
    {
        [Key]
        public string Id { get; set; }
        [Required, MaxLength(50)]
        public string QualificationTypeName { get; set; }
        // Relationships
        public ICollection<Qualification> Qualifications { get; set; }
    }
}
