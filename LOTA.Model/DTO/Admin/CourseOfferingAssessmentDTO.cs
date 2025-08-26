using System;
using System.Collections.Generic;

namespace LOTA.Model.DTO.Admin
{
    public class CourseOfferingAssessmentDTO
    {
        public TrimesterCourse TrimesterCourse { get; set; }
        public ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();
        public ICollection<StudentCourse> Students { get; set; } = new List<StudentCourse>();
    }
}
