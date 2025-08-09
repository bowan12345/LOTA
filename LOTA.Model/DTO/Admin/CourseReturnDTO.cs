using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model.DTO.Admin
{
    /// <summary>
    ///  Data Transfer Object of Course 
    ///  range : backend ->  frontend 
    /// </summary>
    public class CourseReturnDTO
    {
        public string Id { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? QualificationId { get; set; }
        public string? QualificationName { get; set; }
        public string? QualificationType { get; set; }
        public ICollection<LearningOutcomeDTO> LearningOutcomes { get; set; } = new List<LearningOutcomeDTO>();
    }

}
