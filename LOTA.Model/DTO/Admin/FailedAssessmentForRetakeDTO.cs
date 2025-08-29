namespace LOTA.Model.DTO.Admin
{
    public class FailedAssessmentForRetakeDTO
    {
        public string AssessmentId { get; set; }
        public string AssessmentName { get; set; }
        public decimal LOScore { get; set; }
        public decimal MaxLOScore { get; set; }
        public decimal LOPercentage { get; set; }
    }
}
