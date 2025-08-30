using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LOTA.Model.DTO.Admin
{
    public class CourseOfferingDetailsDTO
    {
        public TrimesterCourseInfo TrimesterCourse { get; set; }
        public List<AssessmentInfoDTO> Assessments { get; set; } = new List<AssessmentInfoDTO>();
    }

    public class TrimesterCourseInfo
    {
        public string Id { get; set; }
        public CourseInfoDTO Course { get; set; }
        public TrimesterInfoDTO Trimester { get; set; }
    }

    public class CourseInfoDTO
    {
        public string Id { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
    }

    public class TrimesterInfoDTO
    {
        public string Id { get; set; }
        public int TrimesterNumber { get; set; }
        public int AcademicYear { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class AssessmentInfoDTO
    {
        public string Id { get; set; }
        public string AssessmentName { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public decimal Score { get; set; }
        public DateTime DueDate { get; set; }
        public AssessmentTypeInfoDTO AssessmentType { get; set; }
    }

    public class AssessmentTypeInfoDTO
    {
        public string Id { get; set; }
        public string AssessmentTypeName { get; set; }
        public string Description { get; set; }
    }
}
