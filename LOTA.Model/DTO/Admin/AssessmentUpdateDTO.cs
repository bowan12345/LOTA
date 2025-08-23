using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model.DTO.Admin
{
    public class AssessmentUpdateDTO
    {
        public string Id { get; set; }
        public string CourseId { get; set; }
        public string AssessmentName { get; set; }
        public string AssessmentType { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal TotalScore { get; set; }
        public List<string> LearningOutcomes { get; set; }
    }
}
