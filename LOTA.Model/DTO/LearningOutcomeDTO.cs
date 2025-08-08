using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model.DTO
{
    public class LearningOutcomeDTO
    {
        public string Id { get; set; }
        public string LOName { get; set; }
        public string Description { get; set; }
        public decimal MaxScore { get; set; }
        public decimal Weight { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
