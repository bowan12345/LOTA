using System;
using System.Collections.Generic;

namespace LOTA.Model.DTO.Admin
{
    public class LOResultDTO
    {
        public List<CourseOfferingResultDTO> CourseOfferings { get; set; } = new List<CourseOfferingResultDTO>();
    }

    public class CourseOfferingResultDTO
    {
        public string CourseOfferingId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string TrimesterName { get; set; }
        public List<StudentResultDTO> Students { get; set; } = new List<StudentResultDTO>();
    }

    public class StudentResultDTO
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentNo { get; set; }
        public string StudentEmail { get; set; }
        public List<AssessmentResultDTO> Assessments { get; set; } = new List<AssessmentResultDTO>();
        public decimal TotalScore { get; set; }
        public decimal MaxTotalScore { get; set; }
        public decimal TotalPercentage { get; set; }
        public bool OverallPassed { get; set; }
        public bool NeedsResit { get; set; }
    }

    public class AssessmentResultDTO
    {
        public string AssessmentId { get; set; }
        public string AssessmentName { get; set; }
        public decimal AssessmentScore { get; set; }
        public decimal MaxAssessmentScore { get; set; }
        public decimal AssessmentPercentage { get; set; }
        public bool AssessmentPassed { get; set; }
        public decimal Weight { get; set; }
        public List<LearningOutcomeResultDTO> LearningOutcomes { get; set; } = new List<LearningOutcomeResultDTO>();
    }

    public class LearningOutcomeResultDTO
    {
        public string LearningOutcomeId { get; set; }
        public string LearningOutcomeName { get; set; }
        public string AssessmentLearningOutcomeId { get; set; }
        public decimal LOScore { get; set; }
        public decimal MaxLOScore { get; set; }
        public decimal LOPercentage { get; set; }
        public bool LOPassed { get; set; }
        public bool NeedsRetake { get; set; }
        public bool HasRetake { get; set; }
        public bool HasFailed { get; set; }
        public List<FailedAssessmentDTO> FailedAssessments { get; set; } = new List<FailedAssessmentDTO>();
        
        // Retake-related properties
        public bool IsRetake { get; set; }
        public DateTime? RetakeDate { get; set; }
        public bool RetakePassed { get; set; }
        public bool RetakeFailed { get; set; }
        
        // Historical scores for tooltip
        public List<HistoricalScoreDTO> HistoricalScores { get; set; } = new List<HistoricalScoreDTO>();
    }

    public class HistoricalScoreDTO
    {
        public decimal Score { get; set; }
        public decimal MaxScore { get; set; }
        public DateTime Date { get; set; }
    }

    public class FailedAssessmentDTO
    {
        public string AssessmentId { get; set; }
        public string AssessmentName { get; set; }
        public decimal LOScore { get; set; }
        public decimal MaxLOScore { get; set; }
        public decimal LOPercentage { get; set; }
    }

    public class RetakeScoreDTO
    {
        public string AssessmentId { get; set; }
        public decimal NewScore { get; set; }
        public decimal MaxScore { get; set; }
    }

    public class RetakeRequestDTO
    {
        public string StudentId { get; set; }
        public string LearningOutcomeName { get; set; }
        public string CourseOfferingId { get; set; }
        public List<RetakeScoreDTO> RetakeScores { get; set; } = new List<RetakeScoreDTO>();
    }

    public class RetakeHistoryDTO
    {
        public string AssessmentName { get; set; }
        public decimal OriginalScore { get; set; }
        public decimal RetakeScore { get; set; }
        public decimal MaxScore { get; set; }
        public DateTime RetakeDate { get; set; }
        public bool IsActive { get; set; }
    }
}
