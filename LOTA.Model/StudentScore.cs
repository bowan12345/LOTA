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
    public class StudentScore
    {
        [Key]
        public string Id { get; set; }

        // FKs
        public string StudentNo { get; set; }
        [ForeignKey("StudentNo")]
        public ApplicationUser Student { get; set; }

        public string AssignmentId { get; set; }
        [ForeignKey("AssignmentId")]
        public Assignment Assignment { get; set; }

        public string LOId { get; set; }
        [ForeignKey("LOId")]
        public LearningOutcome LearningOutcome { get; set; }

        // Properties
        [Precision(5, 2)]
        public decimal Score { get; set; }
        [MaxLength(20)]
        public string Status { get; set; }
        public bool?  IsRetake { get; set; } = false;
        public DateTime? RetakeDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
