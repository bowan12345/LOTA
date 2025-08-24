using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model.DTO.Admin
{
    public class AssessmentReturnDTO
    {
        public string? Id { get; set; }

        public string? AssessmentName { get; set; }

        public AssessmentType AssessmentType { get; set; }

        public decimal TotalWeight { get; set; }
        public decimal TotalScore { get; set; }
        public bool IsActive { get; set; } = true;

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public TrimesterCourse? TrimesterCourse { get; set; }
        
        // Trimester information for display
        public Trimester? Trimester { get; set; }

        public ICollection<AssessmentLearningOutcome> AssessmentLearningOutcomes { get; set; } = new List<AssessmentLearningOutcome>();
    }
}
