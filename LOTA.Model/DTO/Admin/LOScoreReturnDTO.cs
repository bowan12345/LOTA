using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LOTA.Model.DTO.Admin
{
    public class LOScoreReturnDTO
    {
        public string Id { get; set; }

        public ApplicationUser Student { get; set; }
        public Assessment Assessment { get; set; }
        public LearningOutcome LearningOutcome { get; set; }
        public Trimester Trimester { get; set; }

        // Properties
        [Precision(5, 2)]
        public decimal Score { get; set; }
        [MaxLength(20)]
        public string Status { get; set; }
        public bool? IsRetake { get; set; } = false;
        public DateTime? RetakeDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

}
