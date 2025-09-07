using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Test
{
    public class AdminLOResultUnitTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly LOResultService _service;

        public AdminLOResultUnitTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new LOResultService(_mockUnitOfWork.Object);
        }


        [Fact]
        public async Task GetLOResultsByCourseOfferingAsync_ReturnsLOResultDTO()
        {
            var course = new Course
            {
                Id = "course1",
                CourseCode = "CS101",
                CourseName = "Intro"
            };

            var trimester = new Trimester { Id = "t1", TrimesterNumber = 1, AcademicYear = 2025 };

            var courseOffering = new TrimesterCourse
            {
                Id = "co1",
                CourseId = "course1",
                Course = course,
                Trimester = trimester
            };

            _mockUnitOfWork.Setup(u => u.trimesterCourseRepository.GetTrimesterCourseWithDetailsAsync("co1"))
                .ReturnsAsync(courseOffering);

            _mockUnitOfWork.Setup(u => u.studentCourseRepository.GetByCourseOfferingIdAsync("co1"))
                .ReturnsAsync(new List<StudentCourse>());

            // Mock batch processing methods with empty data
            _mockUnitOfWork.Setup(u => u.studentRepository.GetByIdsAsync(It.IsAny<List<string>>()))
                .ReturnsAsync(new List<ApplicationUser>());

            // Return empty list for assessments - this should be handled gracefully
            _mockUnitOfWork.Setup(u => u.assessmentRepository.GetAssessmentsWithLOsByCourseOfferingId("co1"))
                .ReturnsAsync(new List<AssessmentWithLOsDTO>());

            _mockUnitOfWork.Setup(u => u.studentScoreRepository.GetStudentScoresByCourseOfferingAsync("co1"))
                .ReturnsAsync(new List<StudentAssessmentScore>());

            _mockUnitOfWork.Setup(u => u.studentLOScoreRepository.GetLOScoresByCourseOfferingAsync("co1"))
                .ReturnsAsync(new List<StudentLOScore>());

            var result = await _service.GetLOResultsByCourseOfferingAsync("co1");

            Assert.NotNull(result);
            Assert.Single(result.CourseOfferings);
            Assert.Empty(result.CourseOfferings[0].Students);
        }

        [Fact]
        public async Task GetFailedAssessmentsForRetakeAsync_ReturnsList()
        {
            // Arrange
            string studentId = "s1";
            string courseOfferingId = "co1";
            string loName = "LO1";

            // Assessment Learning Outcome
            var assessmentLO = new AssessmentLearningOutcome
            {
                Id = "alo1",
                Score = 100m,  // decimal
                LearningOutcome = new LearningOutcome { LOName = loName }
            };

            // Assessment
            var assessment = new Assessment
            {
                Id = "a1",
                AssessmentName = "Test Assessment"
            };

            // StudentAssessmentScore
            var studentAssessmentScore = new StudentAssessmentScore
            {
                Id = "sas1"
            };

            // StudentLOScore
            var studentLOScore = new StudentLOScore
            {
                AssessmentLearningOutcomeId = "alo1",
                Score = 40m, 
                StudentAssessmentScore = studentAssessmentScore
            };

            // Mock repository responses
            _mockUnitOfWork.Setup(u => u.assessmentRepository.GetAssessmentsByCourseOfferingId(courseOfferingId))
                .ReturnsAsync(new List<Assessment> { assessment });

            // Mock GetLOListByAssessmentId to return the assessment LO
            _mockUnitOfWork.Setup(u => u.assessmentRepository.GetLOListByAssessmentId(assessment.Id, It.IsAny<string>()))
                .ReturnsAsync(new List<AssessmentLearningOutcome> { assessmentLO });

            _mockUnitOfWork.Setup(u => u.studentScoreRepository.GetStudentScoreByStudentAssessmentAsync(studentId, "a1"))
                .ReturnsAsync(studentAssessmentScore);

            _mockUnitOfWork.Setup(u => u.studentLOScoreRepository.GetStudentLOScoresByStudentAssessmentScoreAsync("sas1"))
                .ReturnsAsync(new List<StudentLOScore> { studentLOScore });

            // Act
            var result = await _service.GetFailedAssessmentsForRetakeAsync(studentId, courseOfferingId, loName);

            // Assert
            Assert.Single(result);
            var failedAssessment = result[0];
            Assert.Equal("a1", failedAssessment.AssessmentId);
            Assert.Equal("Test Assessment", failedAssessment.AssessmentName);
            Assert.Equal(40m, failedAssessment.LOScore);
            Assert.Equal(100m, failedAssessment.MaxLOScore);
            Assert.Equal(40m, failedAssessment.LOPercentage);
            Assert.True(failedAssessment.LOPercentage < 50m);
        }


        [Fact]
        public async Task UpdateRetakeScoresAsync_Success()
        {
            var studentId = "s1";
            var courseOfferingId = "co1";
            var loName = "LO1";

            var retakeRequest = new RetakeRequestDTO
            {
                StudentId = studentId,
                CourseOfferingId = courseOfferingId,
                LearningOutcomeName = loName,
                RetakeScores = new List<RetakeScoreDTO>
                {
                    new RetakeScoreDTO { AssessmentId = "a1", NewScore = 80, MaxScore = 100 }
                }
            };

            // Assessment Learning Outcome
            var assessmentLO = new AssessmentLearningOutcome
            {
                Id = "alo1",
                Score = 100m,
                LearningOutcome = new LearningOutcome { LOName = loName }
            };

            // Assessment
            var assessment = new Assessment
            {
                Id = "a1",
                AssessmentName = "Test1"
            };

            // StudentAssessmentScore
            var studentAssessmentScore = new StudentAssessmentScore { Id = "sas1" };

            // StudentLOScore
            var studentLOScore = new StudentLOScore
            {
                Id = "slo1",
                AssessmentLearningOutcomeId = "alo1",
                Score = 40m,
                IsActive = true,
                AssessmentLearningOutcome = assessmentLO
            };

            // Mock GetFailedAssessmentsForRetakeAsync to return failed assessment
            _mockUnitOfWork.Setup(u => u.assessmentRepository.GetAssessmentsByCourseOfferingId(courseOfferingId))
                .ReturnsAsync(new List<Assessment> { assessment });

            // Mock GetLOListByAssessmentId to return the assessment LO
            _mockUnitOfWork.Setup(u => u.assessmentRepository.GetLOListByAssessmentId(assessment.Id,It.IsAny<string>()))
                .ReturnsAsync(new List<AssessmentLearningOutcome> { assessmentLO });

            _mockUnitOfWork.Setup(u => u.studentScoreRepository.GetStudentScoreByStudentAssessmentAsync(studentId, "a1"))
                .ReturnsAsync(studentAssessmentScore);

            _mockUnitOfWork.Setup(u => u.studentLOScoreRepository.GetStudentLOScoresByStudentAssessmentScoreAsync("sas1"))
                .ReturnsAsync(new List<StudentLOScore> { studentLOScore });

            _mockUnitOfWork.Setup(u => u.studentLOScoreRepository.AddAsync(It.IsAny<StudentLOScore>()))
                .ReturnsAsync((StudentLOScore s) => s);

            _mockUnitOfWork.Setup(u => u.studentLOScoreRepository.Update(It.IsAny<StudentLOScore>()));

            _mockUnitOfWork.Setup(u => u.SaveAsync())
                .ReturnsAsync(1);

            // Act
            await _service.UpdateRetakeScoresAsync(retakeRequest);

            // Assert
            // Verify that the old score was deactivated
            _mockUnitOfWork.Verify(u => u.studentLOScoreRepository.Update(It.Is<StudentLOScore>(s => s.Id == "slo1" && !s.IsActive)), Times.Once);
            
            // Verify that a new retake score was added
            _mockUnitOfWork.Verify(u => u.studentLOScoreRepository.AddAsync(It.Is<StudentLOScore>(s => 
                s.IsRetake && 
                s.Score == 80m && 
                s.AssessmentLearningOutcomeId == "alo1" &&
                s.StudentAssessmentScoreId == "sas1")), Times.Once);
            
            // Verify that changes were saved
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }
    }
}
