using System.Collections.Generic;

namespace LOTA.Model.DTO.Admin
{
    public class AllStudentsLOScoresBatchSaveDTO
    {
        public string AssessmentId { get; set; }
        public List<StudentLOScoresBatchSaveDTO> StudentScores { get; set; } = new List<StudentLOScoresBatchSaveDTO>();
    }
}
