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
    public class Assignment
    {
        [Key]
        public string? Id { get; set; }

        [Required, MaxLength(50)]
        public string? AssignmentName { get; set; }

        [Precision(5, 2)]
        public decimal TotalWeight { get; set; }
        [Precision(5, 2)]
        public decimal TotalScore { get; set; }
        public bool IsActive { get; set; } = false;

        [MaxLength(50)]
        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // FK
        public string? CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        // Relationships
        public ICollection<AssignmentLearningOutcome> AssignmentLearningOutcomes { get; set; } = new List<AssignmentLearningOutcome>();
        public ICollection<StudentScore> StudentScores { get; set; } = new List<StudentScore>();
    }
}
