using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LOTA.Model
{
    public class Assessment
    {
        [Key]
        public string? Id { get; set; }

        [Required, MaxLength(50)]
        public string? AssessmentName { get; set; }

        public string AssessmentTypeId { get; set; }
        [ForeignKey("AssessmentTypeId")]
        public AssessmentType AssessmentType { get; set; }


        [Precision(5, 2)]
        public decimal Weight { get; set; }
        [Precision(5, 2)]
        public decimal Score { get; set; }
        public bool IsActive { get; set; } = false;

        [MaxLength(50)]
        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // FK
        public string? CourseOfferingId { get; set; }
        [ForeignKey("CourseOfferingId")]
        public TrimesterCourse? TrimesterCourse { get; set; }

        public string TrimesterId { get; set; }
        [ForeignKey("TrimesterId")]
        public Trimester Trimester { get; set; }

        // Relationships
        public ICollection<AssessmentLearningOutcome> AssessmentLearningOutcomes { get; set; } = new List<AssessmentLearningOutcome>();
    }


    public class AssessmentType
    {
        [Key]
        public string Id { get; set; }
        [Required, MaxLength(50)]
        public string AssessmentTypeName { get; set; }
    }
}
