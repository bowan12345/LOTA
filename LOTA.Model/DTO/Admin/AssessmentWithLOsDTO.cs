using System.Collections.Generic;

namespace LOTA.Model.DTO.Admin
{
    public class AssessmentWithLOsDTO
    {
        public string AssessmentId { get; set; }
        public string AssessmentName { get; set; }
        public decimal MaxAssessmentScore { get; set; }
        public decimal Weight { get; set; }
        public List<AssessmentLOWithScoreDTO> AssessmentLearningOutcomes { get; set; } = new List<AssessmentLOWithScoreDTO>();
    }

    public class AssessmentLOWithScoreDTO
    {
        public string Id { get; set; }
        public string LOId { get; set; }
        public decimal Score { get; set; }
        public LearningOutcome LearningOutcome { get; set; }
    }
}
