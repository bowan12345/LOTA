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
    public class LearningOutcome
    {
        [Key]
        public string Id { get; set; }

        [Required, MaxLength(50)]
        public string LOName { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Precision(5, 2)]
        public decimal MaxScore { get; set; }
        [Precision(5, 2)]
        public decimal Weight { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // FK
        public string CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        // Relationship
        public ICollection<AssessmentLearningOutcome> AssessmentLearningOutcomes { get; set; } = new List<AssessmentLearningOutcome>();
    }
}
