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
    public class StudentLOScore
    {
        [Key]
        public string Id { get; set; }

        // FKs
        public string StudentAssessmentScoreId { get; set; }
        [ForeignKey("StudentAssessmentScoreId")]
        public StudentAssessmentScore StudentAssessmentScore { get; set; }

        // Properties
        [Precision(5, 2)]
        public decimal Score { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
