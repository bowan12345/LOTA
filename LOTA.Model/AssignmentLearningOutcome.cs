using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model
{
    public class AssignmentLearningOutcome
    {
        [Key]
        public string? Id { get; set; }
        public string? AssignmentId { get; set; }
        [ForeignKey("AssignmentId")]
        public Assignment? Assignment { get; set; }

        public string? LOId { get; set; }
        [ForeignKey("LOId")]
        public LearningOutcome? LearningOutcome { get; set; }
    }
}
