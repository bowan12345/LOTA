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
    public class StudentAssessmentScore
    {
        [Key]
        public string Id { get; set; }

        // FKs
        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public ApplicationUser Student { get; set; }

        public string AssessmentId { get; set; }
        [ForeignKey("AssessmentId")]
        public Assessment Assessment { get; set; }

        // Properties
        [Precision(5, 2)]
        public decimal TotalScore { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<StudentLOScore> StudentLOScores { get; set; } = new List<StudentLOScore>();
    }
}
