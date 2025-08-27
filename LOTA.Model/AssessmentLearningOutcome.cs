using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model
{
    public class AssessmentLearningOutcome
    {
        [Key]
        public string? Id { get; set; }
        public string? AssessmentId { get; set; }
        [ForeignKey("AssessmentId")]
        public Assessment? Assessment { get; set; }

        public string? LOId { get; set; }
        [ForeignKey("LOId")]
        public LearningOutcome? LearningOutcome { get; set; }

        [Precision(5, 2)]
        public decimal Score { get; set; }
    }
}
