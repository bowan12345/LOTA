using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model.DTO.Admin
{
    public class LOScoreCreateDTO
    {
        [Required]
        public string StudentId { get; set; }

        [Required]
        public string AssessmentId { get; set; }

        [Required]
        public string LOId { get; set; }

        [Required]
        public string TrimesterId { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
        public decimal Score { get; set; }

        public string Status { get; set; } = "Active";
        public bool IsRetake { get; set; } = false;
        public DateTime? RetakeDate { get; set; }
    }
}
