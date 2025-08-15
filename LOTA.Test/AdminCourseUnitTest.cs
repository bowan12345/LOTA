using LOTA.DataAccess.Repository;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service;
using Moq;
using System.Linq.Expressions;

/*[Test] call Service
   │
   │
CourseService.CreateCourseAsync(courseDTO)
   │
   │ 1️⃣ create course object
   │
   │
await _unitOfWork.courseRepository.AddAsync(course)
   │
   │---> without Setup：
   │         _unitOfWork.courseRepository is Mock data
   │         AddAsync default return value is null
   │         await null → NullReferenceException 
   │
   │---> with Setup：
   │         AddAsync return value is  Task<Course>
   │         await without error
   │
   │
if (courseDTO.LearningOutcomes != null)
   │
   │
await _unitOfWork.learningOutcomeRepository.AddRangeAsync(loList)
   │
   │---> without Setup：
   │         AddRangeAsync default return value is null
   │         await null → NullReferenceException 
   │
   │---> with Setup：
   │         AddRangeAsync return value is Task<Learningoutcome>
   │         await without error
   │     Task<T> method，using ReturnsAsync(return object)； Task method，using Returns(Task.CompletedTask)。
   │
await _unitOfWork.SaveAsync()
   │
   │
return CourseReturnDTO*/

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
            mockUnitOfWork.Setup(u => u.courseRepository.GetAllAsync(It.IsAny<Expression<Func<Course, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(new List<Course>
                {
                new Course { Id = "1", CourseName = "DevOps" },
                new Course { Id = "2", CourseName = "Programming" }
                });

            var service = new CourseService(mockUnitOfWork.Object);

            // Act
            var result = await service.GetAllCoursesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.CourseName == "DevOps");
        }

        [Fact]
        public async Task CreateCourseAsync_WithExistingCourses_ReturnsCreatedCourses()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(r => r.courseRepository.AddAsync(It.IsAny<Course>()))
                  .ReturnsAsync((Course c) => c); 

            mockUnitOfWork.Setup(r => r.learningOutcomeRepository.AddRangeAsync(It.IsAny<IEnumerable<LearningOutcome>>()))
                      .ReturnsAsync(new List<LearningOutcome>());

            var service = new CourseService(mockUnitOfWork.Object);
            var dto = new CourseCreateDTO
            {
                CourseName = "DevOps",
                CourseCode = "DV01",
                Description = "DevOps course",
                QualificationId = "Q1",
                LearningOutcomes = new List<LearningOutcomeCreateDTO>
                {
                    new LearningOutcomeCreateDTO { LOName = "Intro", Description = "Basics" }
                }
            };
            // Act
            var result = await service.CreateCourseAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.CourseName, result.CourseName);
            Assert.Equal(dto.CourseCode, result.CourseCode);
        }

        [Fact]
        public async Task GetCourseByCodeAsync_WithExistingCourses_ReturnsCourse()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var fakeCourse = new Course
            {
                Id = "1",
                CourseCode = "TEST100",
                CourseName = "Test Course"
            };
            mockUnitOfWork.Setup(u => u.courseRepository.GetCourseByCodeAsync("TEST100")).ReturnsAsync(fakeCourse);

            var service = new CourseService(mockUnitOfWork.Object);

            // Act
            var result = await service.GetCourseByCodeAsync("TEST100");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TEST100", result.CourseCode);
            Assert.Equal("Test Course", result.CourseName);
        }

        [Fact]
        public async Task GetCourseByIdAsync_WithExistingCourses_ReturnsCourse()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var fakeCourse = new Course
            {
                Id = "1",
                CourseCode = "TEST100",
                CourseName = "Test Course"
            };
            mockUnitOfWork.Setup(u => u.courseRepository.GetByIdAsync("1", It.Is<string>(p => p == "LearningOutcomes,Qualification,Qualification.QualificationType"))).ReturnsAsync(fakeCourse);

            var service = new CourseService(mockUnitOfWork.Object);

            // Act
            var result = await service.GetCourseByIdAsync("1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TEST100", result.CourseCode);
            Assert.Equal("Test Course", result.CourseName);
        }

        [Fact]
        public async Task GetCoursesByNameOrCodeAsync_WithExistingCourses_ReturnsCourse()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var fakeCourse = new List<Course>
            {
                new Course{ Id = "1",
                CourseCode = "TEST100",
                CourseName = "Test Course"
                },
                new Course{ Id = "2",
                CourseCode = "TEST102",
                CourseName = "Test Course2"
                }
            };
            mockUnitOfWork.Setup(u => u.courseRepository.GetAllAsync(It.IsAny<Expression<Func<Course, bool>>>(),
                It.Is<string>(p => p == "LearningOutcomes,Qualification,Qualification.QualificationType"))).ReturnsAsync(fakeCourse);

            var service = new CourseService(mockUnitOfWork.Object);

            // Act
            var result = await service.GetCoursesByNameOrCodeAsync("TEST100");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Course", result.First().CourseName);
        }

        [Fact]
        public async Task RemoveCourse_ValidId_CallsRemoveAndSaveAsync()
        {
            // Arrange
            Mock<ICourseRepository> courseRepositoryMock = new Mock<ICourseRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            
            mockUnitOfWork.Setup(u => u.courseRepository).Returns(courseRepositoryMock.Object);
            mockUnitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(1);

            var service = new CourseService(mockUnitOfWork.Object);
            var testCourseId = "course-123";

            // Act
            await service.RemoveCourse("course-123");

            // Assert
            // Verify that Remove is called once and that the Course object Id passed in is the expected value
            courseRepositoryMock.Verify(r => r.Remove(It.Is<string>(id => id == testCourseId)), Times.Once);
            // Verify that SaveAsync is called once
            mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }



        [Fact]
        public async Task UpdateCourse_ValidId_CallsRemoveAndSaveAsync()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            Mock<ICourseRepository> courseRepositoryMock = new Mock<ICourseRepository>();
            Mock<ILearningOutcomeRepository> learningOutcomeRepositoryMock = new Mock<ILearningOutcomeRepository>();
            mockUnitOfWork.Setup(u => u.courseRepository).Returns(courseRepositoryMock.Object);
            mockUnitOfWork.Setup(u => u.learningOutcomeRepository).Returns(learningOutcomeRepositoryMock.Object);
            mockUnitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(1);
            var existingCourse = new Course { Id = "testCourseId" };
            var existingLOs = new List<LearningOutcome>();
            // Mock GetByIdAsync return existingCourse
            courseRepositoryMock.Setup(r => r.GetByIdAsync("testCourseId", It.IsAny<string>()))
                .ReturnsAsync(existingCourse);

            // Mock GetAllAsync return LexistingLOs
            learningOutcomeRepositoryMock.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<LearningOutcome, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(existingLOs);

            var service = new CourseService(mockUnitOfWork.Object);
            var dto = new CourseUpdateDTO
            {
                Id= "testCourseId",
                CourseName = "DevOps",
                CourseCode = "DV01",
                Description = "DevOps course",
                QualificationId = "Q1",
                LearningOutcomes = new List<LearningOutcomeUpdateDTO>
                {
                    new LearningOutcomeUpdateDTO { LOName = "Intro", Description = "Basics" }
                }
            };
          
            // Act
            await service.UpdateCourse(dto);
            // Assert
            // Verify that Remove is called once and that the Course object Id passed in is the expected value
            courseRepositoryMock.Verify(r => r.Update(It.Is<Course>(c => c.Id == dto.Id)), Times.Once);
            // Verify that SaveAsync is called once
            mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }


        [Fact]
        public async Task AddStudentsToCourseAsync_ShouldAddStudents_WhenNotEnrolled()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockTrimesterRepo = new Mock<ITrimesterRepository>();
            var mockStudentCourseRepo = new Mock<IStudentCourseRepository>();

            string courseId = "course1";
            string trimesterId = "trimester1";
            var studentIds = new List<string> { "student1", "student2" };

            mockTrimesterRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Trimester, bool>>>(), It.IsAny<string>()))
           .ReturnsAsync(new List<Trimester> { new Trimester { Id = trimesterId } });

            mockStudentCourseRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<StudentCourse, bool>>>(), It.IsAny<string>()))
           .ReturnsAsync(new List<StudentCourse>());

            mockUnitOfWork.Setup(u => u.trimesterRepository).Returns(mockTrimesterRepo.Object);
            mockUnitOfWork.Setup(u => u.studentCourseRepository).Returns(mockStudentCourseRepo.Object);

            var service = new CourseService(mockUnitOfWork.Object);
            
            // Act
            await service.AddStudentsToCourseAsync(courseId, studentIds,trimesterId);

            // Assert
            // Verify that Remove is called once and that the Course object Id passed in is the expected value
            //mockTrimesterRepo.Verify(r => r.GetAsync(It.Is<Trimester>(c => c.Id == trimesterId), It.IsAny<string>()), Times.Once);
            // Verify that SaveAsync is called once
            mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }


        [Fact]
        public async Task RemoveStudentFromCourseAsync_ValidId_CallsRemoveAndSaveAsync()
        {
            // Arrange
            Mock<IStudentCourseRepository> studentCourseRepositoryMock = new Mock<IStudentCourseRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var testCourseId = "course-123";
            string studentId = "student123";
            var enrollment = new StudentCourse { CourseId = testCourseId, StudentId = studentId };
            mockUnitOfWork.Setup(u => u.studentCourseRepository).Returns(studentCourseRepositoryMock.Object);
            mockUnitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(1);

            studentCourseRepositoryMock
                .Setup(r => r.GetByStudentAndCourseAsync(studentId, testCourseId))
                .ReturnsAsync(enrollment);
            var service = new CourseService(mockUnitOfWork.Object);
            // Act
            await service.RemoveStudentFromCourseAsync(testCourseId, studentId);

            // Assert
            // Verify that Remove is called once and that the Course object Id passed in is the expected value
            studentCourseRepositoryMock.Verify(r => r.Remove(It.Is<string>(id => id == enrollment.Id)), Times.Once);
            // Verify that SaveAsync is called once
            mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }
    }
}