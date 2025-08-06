using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Service.Service;
using Moq;

namespace LOTA.Test
{
    /// <summary>
    /// (happy path tests)unit tests for admin functionality
    /// </summary>
    public class AdminCourseUnitTest
    {
        [Fact]
        public async Task GetAllCoursesAsync_WithExistingCourses_ReturnsAllCourses()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.courseRepository.GetAllAsync(null, null))
                .ReturnsAsync(new List<Course>
                {
                new Course { Id = "1", CourseName = "DevOps" },
                new Course { Id = "2", CourseName = "Programming" }
                });

            var service = new CourseService(mockUnitOfWork.Object);

            // Act
            var result = await service.GetAllCoursesAsync(null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.CourseName == "DevOps");
        }
    }
}