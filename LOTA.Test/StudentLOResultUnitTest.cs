using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model.DTO.Admin;
using LOTA.Model;
using LOTA.Service.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Test
{
    public class StudentLOResultUnitTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly LOResultService _service;

        public StudentLOResultUnitTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new LOResultService(_mockUnitOfWork.Object);
        }


        [Fact]
        public async Task GetStudentLOResultsAsync_ReturnsStudentLOResult()
        {
            // Arrange
            var studentId = "s1";

            var student = new ApplicationUser
            {
                Id = studentId,
                FirstName = "John",
                LastName = "Doe",
                StudentNo = "12345",
                Email = "john.doe@example.com"
            };

            var trimester = new Trimester { Id = "tr1", TrimesterNumber = 1, AcademicYear = 2025 };
            var course = new Course { Id = "c1", CourseName = "Math", CourseCode = "M101" };

            var trimesterCourse = new TrimesterCourse
            {
                Id = "tc1",
                Course = course,
                Trimester = trimester
            };

            var assessmentType = new AssessmentType { Id = "at1", AssessmentTypeName = "Exam" };
            var assessment = new Assessment
            {
                Id = "a1",
                AssessmentName = "Midterm",
                Score = 100m,
                Weight = 50m,
                AssessmentType = assessmentType,
                TrimesterCourse = trimesterCourse
            };

            var studentAssessmentScore = new StudentAssessmentScore
            {
                Id = "sas1",
                Assessment = assessment,
                AssessmentId = "a1",
                TotalScore = 60m
            };

            var learningOutcome = new LearningOutcome { Id = "lo1", LOName = "Understand Algebra" };
            var assessmentLO = new AssessmentLearningOutcome
            {
                Id = "alo1",
                Score = 100m,
                LearningOutcome = learningOutcome
            };

            var studentLOScore = new StudentLOScore
            {
                Id = "slo1",
                AssessmentLearningOutcome = assessmentLO,
                AssessmentLearningOutcomeId = "alo1",
                StudentAssessmentScore = studentAssessmentScore,
                StudentAssessmentScoreId = "sas1",
                Score = 60m,
                IsRetake = false
            };

            _mockUnitOfWork.Setup(u => u.studentRepository.GetByIdAsync(studentId, null))
                .ReturnsAsync(student);

            _mockUnitOfWork.Setup(u => u.studentLOScoreRepository.GetStudentLOScoresWithDetailsAsync(studentId))
                .ReturnsAsync(new List<StudentLOScore> { studentLOScore });

            // Act
            var result = await _service.GetStudentLOResultsAsync(studentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(studentId, result.StudentId);
            Assert.Equal("John Doe", result.StudentName);
            Assert.Equal("12345", result.StudentNo);

            var trimesterResult = Assert.Single(result.Trimesters);
            Assert.Equal("Trimester 1 2025", trimesterResult.TrimesterName);

            var courseOffering = Assert.Single(trimesterResult.CourseOfferings);
            Assert.Equal("Math", courseOffering.CourseName);
            Assert.Equal("M101", courseOffering.CourseCode);

            var assessmentResult = Assert.Single(courseOffering.Assessments);
            Assert.Equal("Midterm", assessmentResult.AssessmentName);
            Assert.Equal("Exam", assessmentResult.AssessmentType);
            Assert.Equal(60m, assessmentResult.AssessmentScore);
            Assert.Equal(100m, assessmentResult.MaxAssessmentScore);
            Assert.True(assessmentResult.AssessmentPassed);

            var loResult = Assert.Single(assessmentResult.LearningOutcomes);
            Assert.Equal("Understand Algebra", loResult.LearningOutcomeName);
            Assert.Equal(60m, loResult.LOScore);
            Assert.Equal(100m, loResult.MaxLOScore);
            Assert.True(loResult.LOPassed);
            Assert.False(loResult.NeedsRetake);
        }
    }
}
