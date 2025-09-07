using System;
using System.Collections.Generic;
using LOTA.Model.DTO.Admin;

namespace LOTA.Model.DTO.Student
{
    public class StudentLOResultDTO
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentNo { get; set; }
        public string StudentEmail { get; set; }
        public List<TrimesterResultDTO> Trimesters { get; set; } = new List<TrimesterResultDTO>();
    }

    public class TrimesterResultDTO
    {
        public string TrimesterId { get; set; }
        public string TrimesterName { get; set; }
        public int TrimesterNumber { get; set; }
        public string AcademicYear { get; set; }
        public List<StudentCourseOfferingResultDTO> CourseOfferings { get; set; } = new List<StudentCourseOfferingResultDTO>();
    }

    public class StudentCourseOfferingResultDTO
    {
        public string CourseOfferingId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public List<AssessmentResultDTO> Assessments { get; set; } = new List<AssessmentResultDTO>();
        public decimal TotalScore { get; set; }
        public decimal MaxTotalScore { get; set; }
    }
}
