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
    public class AdminLOScoreUnitTest
    {
        private readonly LOScoreService _service;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public AdminLOScoreUnitTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            // Mock repositories
            _mockUnitOfWork.Setup(u => u.trimesterRepository).Returns(Mock.Of<ITrimesterRepository>());
            _mockUnitOfWork.Setup(u => u.trimesterCourseRepository).Returns(Mock.Of<ITrimesterCourseRepository>());
            _mockUnitOfWork.Setup(u => u.assessmentRepository).Returns(Mock.Of<IAssessmentRepository>());
            _mockUnitOfWork.Setup(u => u.studentCourseRepository).Returns(Mock.Of<IStudentCourseRepository>());
            _mockUnitOfWork.Setup(u => u.studentScoreRepository).Returns(Mock.Of<IStudentScoreRepository>());
            _mockUnitOfWork.Setup(u => u.studentLOScoreRepository).Returns(Mock.Of<IStudentLOScoreRepository>());
            _mockUnitOfWork.Setup(u => u.studentRepository).Returns(Mock.Of<IStudentRepository>());

            _service = new LOScoreService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetCourseOfferingsWithAssessmentsAsync_ShouldReturnResult()
        {
            // Arrange
            var trimester = new Trimester { Id = "t1" };
            var courseOffering = new TrimesterCourse { Id = "c1" };
            _mockUnitOfWork.Setup(u => u.trimesterRepository.GetLatestTrimestersAsync())
                .ReturnsAsync(trimester);
            _mockUnitOfWork.Setup(u => u.trimesterCourseRepository.GetTrimesterCoursesByTrimesterAsync("t1"))
                .ReturnsAsync(new List<TrimesterCourse> { courseOffering });
            _mockUnitOfWork.Setup(u => u.assessmentRepository.GetAssessmentsByCourseOfferingId("c1"))
                .ReturnsAsync(new List<Assessment>());
            _mockUnitOfWork.Setup(u => u.studentCourseRepository.GetByCourseOfferingIdAsync("c1"))
                .ReturnsAsync(new List<StudentCourse>());

            // Act
            var result = await _service.GetCourseOfferingsWithAssessmentsAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(courseOffering.Id, result.First().TrimesterCourse.Id);
            Assert.Empty(result.First().Assessments);
            Assert.Empty(result.First().Students);
        }

        [Fact]
        public async Task GetCourseOfferingWithAssessmentsAsync_ShouldReturnResult()
        {
            // Arrange
            var courseOffering = new TrimesterCourse { Id = "c1" };
            _mockUnitOfWork.Setup(u => u.trimesterCourseRepository.GetTrimesterCourseByIdAsync("c1"))
                .ReturnsAsync(courseOffering);
            _mockUnitOfWork.Setup(u => u.assessmentRepository.GetAssessmentsByCourseOfferingId("c1"))
                .ReturnsAsync(new List<Assessment>());
            _mockUnitOfWork.Setup(u => u.studentCourseRepository.GetByCourseOfferingIdAsync("c1"))
                .ReturnsAsync(new List<StudentCourse>());
            _mockUnitOfWork.Setup(u => u.studentScoreRepository.GetStudentScoresByCourseOfferingAsync("c1"))
                .ReturnsAsync(new List<StudentAssessmentScore>());
            _mockUnitOfWork.Setup(u => u.studentLOScoreRepository.GetStudentLOScoresByStudentAssessmentScoreAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<StudentLOScore>());

            // Act
            var result = await _service.GetCourseOfferingWithAssessmentsAsync("c1");

            // Assert
            Assert.Equal(courseOffering.Id, result.TrimesterCourse.Id);
            Assert.Empty(result.Assessments);
            Assert.Empty(result.Students);
            Assert.Empty(result.StudentAssessmentScores);
        }

        [Fact]
        public async Task BatchSaveStudentLOScoresAsync_ShouldCallSaveAsync()
        {
            // Arrange
            var loScoreDto = new LOScoreCreateDTO { AssessmentLearningOutcomeId = "lo1", Score = 5 };
            var loScores = new List<LOScoreCreateDTO> { loScoreDto };
            _mockUnitOfWork.Setup(u => u.assessmentRepository.GetByIdAsync("a1", It.IsAny<string>()))
                .ReturnsAsync(new Assessment { Id = "a1" });
            _mockUnitOfWork.Setup(u => u.assessmentRepository.GetLOListByAssessmentId("a1",It.IsAny<string>()))
                .ReturnsAsync(new List<AssessmentLearningOutcome> { new AssessmentLearningOutcome { Id = "lo1", Score = 10 } });
            _mockUnitOfWork.Setup(u => u.studentScoreRepository.GetStudentScoreByStudentAssessmentAsync("s1", "a1"))
                .ReturnsAsync((StudentAssessmentScore)null);
            _mockUnitOfWork.Setup(u => u.studentScoreRepository.AddAsync(It.IsAny<StudentAssessmentScore>()))
                .ReturnsAsync((StudentAssessmentScore s) => s);
            _mockUnitOfWork.Setup(u => u.studentLOScoreRepository.AddAsync(It.IsAny<StudentLOScore>()))
                .ReturnsAsync((StudentLOScore s) => s);
            _mockUnitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(1);

            // Act
            await _service.BatchSaveStudentLOScoresAsync("s1", "a1", loScores);

            // Assert
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }
    }
}
