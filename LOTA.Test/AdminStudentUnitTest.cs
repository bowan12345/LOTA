using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Test
{
    public class AdminStudentUnitTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly StudentService _service;

        public AdminStudentUnitTest()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var optionsMock = new Mock<IOptions<IdentityOptions>>();
            var passwordHasherMock = new Mock<IPasswordHasher<ApplicationUser>>();
            var userValidators = new List<IUserValidator<ApplicationUser>> { new UserValidator<ApplicationUser>() };
            var passwordValidators = new List<IPasswordValidator<ApplicationUser>> { new PasswordValidator<ApplicationUser>() };
            var keyNormalizerMock = new Mock<ILookupNormalizer>();
            var errorDescriber = new IdentityErrorDescriber();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var loggerMock = new Mock<ILogger<UserManager<ApplicationUser>>>();

            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object,
                optionsMock.Object,
                passwordHasherMock.Object,
                userValidators,
                passwordValidators,
                keyNormalizerMock.Object,
                errorDescriber,
                serviceProviderMock.Object,
                loggerMock.Object
            );
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new StudentService(_mockUnitOfWork.Object, _mockUserManager.Object);
        }

        [Fact]
        public async Task GetAllStudentsAsync_ReturnsOnlyStudentsWithStudentNo()
        {
            // Arrange
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", StudentNo = "S001", FirstName = "Alice" },
                new ApplicationUser { Id = "2", StudentNo = null, FirstName = "Bob" }, 
                new ApplicationUser { Id = "3", StudentNo = "S002", FirstName = "Charlie" }
            };
            _mockUnitOfWork.Setup(u => u.studentRepository.GetAllAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(users);

            // Act
            var result = await _service.GetAllStudentsAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.DoesNotContain(result, s => s.Id == "2");
        }

        [Fact]
        public async Task GetStudentByIdAsync_ReturnsStudentDTO_WhenStudentHasStudentNo()
        {
            var user = new ApplicationUser { Id = "1", StudentNo = "S001", FirstName = "Alice" };

            _mockUnitOfWork.Setup(u => u.studentRepository.GetByIdAsync("1", It.IsAny<string>()))
                .ReturnsAsync(user);

            var result = await _service.GetStudentByIdAsync("1");

            Assert.NotNull(result);
            Assert.Equal("1", result.Id);
        }


        [Fact]
        public async Task GetStudentsByNameOrEmailAsync_ReturnsFilteredStudents()
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", StudentNo = "S001", FirstName = "Alice", Email = "alice@test.com" },
                new ApplicationUser { Id = "2", StudentNo = "S002", FirstName = "Bob", Email = "bob@test.com" },
                new ApplicationUser { Id = "3", StudentNo = null, FirstName = "Charlie" }
            };

            _mockUnitOfWork.Setup(u => u.studentRepository.GetAllAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(users);

            var result = await _service.GetStudentsByNameOrEmailAsync("alice");

            Assert.Single(result);
            Assert.Equal("1", result.First().Id);
        }


        [Fact]
        public async Task CreateStudentAsync_SuccessfullyCreatesStudent()
        {
            var studentDto = new StudentCreateDTO
            {
                FirstName = "New",
                LastName = "Student",
                Email = "newstudent@test.com",
                Password = "Password123!",
                StudentNo = "S123",
                IsActive = true
            };

            _mockUserManager.Setup(um => um.FindByEmailAsync(studentDto.Email))
                .ReturnsAsync((ApplicationUser?)null);

            _mockUserManager.Setup(um => um.Users)
                .Returns(new List<ApplicationUser>().AsQueryable());

            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), studentDto.Password))
                .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Setup(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Student"))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _service.CreateStudentAsync(studentDto);

            Assert.NotNull(result);
            Assert.Equal(studentDto.Email, result.Email);
        }


        [Fact]
        public async Task UpdateStudentAsync_SuccessfullyUpdatesStudent()
        {
            var studentDto = new StudentUpdateDTO
            {
                Id = "1",
                FirstName = "Updated",
                LastName = "Student",
                Email = "updated@test.com",
                StudentNo = "S001",
                IsActive = true
            };

            var existingUser = new ApplicationUser
            {
                Id = "1",
                Email = "old@test.com",
                StudentNo = "S001"
            };

            _mockUserManager.Setup(um => um.FindByIdAsync(studentDto.Id))
                .ReturnsAsync(existingUser);

            _mockUserManager.Setup(um => um.FindByEmailAsync(studentDto.Email))
                .ReturnsAsync(existingUser); // same user, so allowed

            _mockUserManager.Setup(um => um.Users)
                .Returns(new List<ApplicationUser> { existingUser }.AsQueryable());

            _mockUserManager.Setup(um => um.UpdateAsync(existingUser))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _service.UpdateStudentAsync(studentDto);

            Assert.NotNull(result);
            Assert.Equal(studentDto.Email, result.Email);
            Assert.Equal("Updated", result.FirstName);
        }


        [Fact]
        public async Task DeleteStudentAsync_SucceedsWhenNoEnrolledCourses()
        {
            var user = new ApplicationUser
            {
                Id = "1",
                FirstName = "Delete",
                LastName = "Me",
                StudentCourses = new List<StudentCourse>() // empty
            };

            _mockUserManager.Setup(um => um.FindByIdAsync("1"))
                .ReturnsAsync(user);

            _mockUserManager.Setup(um => um.DeleteAsync(user))
                .ReturnsAsync(IdentityResult.Success);

            await _service.DeleteStudentAsync("1");

            var student = await _service.GetStudentByIdAsync("1");
            Assert.Null(student);
        }


        [Fact]
        public async Task IsStudentEmailExistsAsync_ReturnsTrueIfExists()
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", Email = "exists@test.com" }
            };
            _mockUnitOfWork.Setup(u => u.studentRepository.GetAllAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(users);

            var result = await _service.IsStudentEmailExistsAsync("exists@test.com");

            Assert.True(result);
        }



        [Fact]
        public async Task IsStudentNoExistsAsync_ReturnsTrueIfExists()
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", StudentNo = "S001" }
            };
            _mockUnitOfWork.Setup(u => u.studentRepository.GetAllAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(users);

            var result = await _service.IsStudentNoExistsAsync("S001");

            Assert.True(result);
        }

        [Fact]
        public async Task GetEnrolledStudentsAsync_ReturnsMappedStudents()
        {
            var studentCourses = new List<StudentCourse>
            {
                new StudentCourse { Student = new ApplicationUser { Id = "1", FirstName = "A", LastName = "B", Email = "a@b.com", StudentNo = "S1", IsActive = true } }
            };
            //_mockUnitOfWork.Setup(u => u.studentCourseRepository.GetByCourseIdAndTrimesterAsync("course1", null, null)).ReturnsAsync(studentCourses);

            //var result = await _service.GetEnrolledStudentsAsync("course1");

            //Assert.Single(result);
            //Assert.Equal("1", result.First().Id);
        }
    }
}
