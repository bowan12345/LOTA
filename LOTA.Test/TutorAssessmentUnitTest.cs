using LOTA.DataAccess.Repository.IRepository;
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
    public class TutorAssessmentUnitTest
    {

        [Fact]
        public async Task GetAllAssessmentsBuTutorIdAsync_ReturnsAssessmentsForTutor()
        {
            // Arrange
            var tutorId = "tutor1";

            // Trimester
            var trimester = new Trimester
            {
                Id = "trim1",
                TrimesterNumber = 1,
                AcademicYear = 2025
            };

            // Course
            var course = new Course
            {
                Id = "course1",
                CourseName = "Algorithms",
                CourseCode = "CS101"
            };

            // TrimesterCourse
            var trimesterCourse = new TrimesterCourse
            {
                Id = "tc1",
                TutorId = tutorId,
                Course = course,
                Trimester = trimester
            };

            // AssessmentType
            var assessmentType = new AssessmentType
            {
                Id = "atype1",
                AssessmentTypeName = "Assignment"
            };

            // Assessment
            var assessment = new Assessment
            {
                Id = "a1",
                AssessmentName = "Assignment 1",
                TrimesterId = trimester.Id,
                TrimesterCourse = trimesterCourse,
                Trimester = trimester,
                AssessmentType = assessmentType
            };

            // Mock repository
            var mockTrimesterRepo = new Mock<ITrimesterRepository>();
            mockTrimesterRepo
                .Setup(r => r.GetLatestTrimestersAsync())
                .ReturnsAsync(trimester);

            var mockAssessmentRepo = new Mock<IAssessmentRepository>();
            mockAssessmentRepo
                .Setup(r => r.GetAllAsync(
                    It.IsAny<System.Linq.Expressions.Expression<Func<Assessment, bool>>>(),
                    It.IsAny<string>()))
                .ReturnsAsync(new List<Assessment> { assessment });

            // Mock UnitOfWork
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.trimesterRepository).Returns(mockTrimesterRepo.Object);
            mockUnitOfWork.Setup(u => u.assessmentRepository).Returns(mockAssessmentRepo.Object);

            var service = new AssessmentService(mockUnitOfWork.Object);

            // Act
            var result = await service.GetAllAssessmentsBuTutorIdAsync(tutorId);

            // Assert
            var resultList = result.ToList();
            Assert.Single(resultList);

            var dto = resultList[0];
            Assert.Equal("a1", dto.Id);
            Assert.Equal("Assignment 1", dto.AssessmentName);
            Assert.Equal("Assignment", dto.AssessmentType.AssessmentTypeName);
        }

    }
}
