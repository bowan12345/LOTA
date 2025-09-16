using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Service.Service;
using LOTA.Service.Service.IService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Test
{
    public class TutorCourseUnitTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ITrimesterCourseRepository> _trimesterCourseRepo;
        private readonly ITrimesterCourseService _trimesterCourseService;

        public TutorCourseUnitTest() 
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _trimesterCourseRepo = new Mock<ITrimesterCourseRepository>();

            // Setup repositories in unit of work mock
            _mockUnitOfWork.SetupGet(u => u.trimesterCourseRepository).Returns(_trimesterCourseRepo.Object);
            _trimesterCourseService = new TrimesterCourseService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetTrimesterCoursesByTutorAndTrimesterAsync_ReturnsTrimesterCourses()
        {
            // Arrange
            var tutorId = "t1";
            var trimesterId = "tr1";

            var trimesterCourse = new TrimesterCourse
            {
                Id = "tc1",
                TutorId = tutorId,
                TrimesterId = trimesterId,
                Course = new Course { Id = "c1", CourseName = "Math", CourseCode = "M101" },
                Trimester = new Trimester { Id = trimesterId, TrimesterNumber = 1, AcademicYear = 2025 }
            };

            // _trimesterCourseRepo
            _trimesterCourseRepo
                .Setup(r => r.GetTrimesterCoursesByTutorAndTrimesterAsync(tutorId, trimesterId))
                .ReturnsAsync(new List<TrimesterCourse> { trimesterCourse });

            // Act
            var result = await _trimesterCourseService.GetTrimesterCoursesByTutorAndTrimesterAsync(tutorId, trimesterId);

            // Assert
            var dto = Assert.Single(result);
            Assert.Equal("tc1", dto.Id);
            Assert.Equal("Math", dto.Course.CourseName);
            Assert.Equal("M101", dto.Course.CourseCode);
            Assert.Equal(1, dto.Trimester.TrimesterNumber);
        }
    }
}
