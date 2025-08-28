using System.Collections.Generic;

namespace LOTA.Model.DTO.Admin
{
    public class BatchSaveStudentLOScoresDTO
    {
        public string StudentId { get; set; }
        public string AssessmentId { get; set; }
        public List<LOScoreCreateDTO> LOScores { get; set; } = new List<LOScoreCreateDTO>();
    }
}
