using LOTA.DataAccess.Repository;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Service.Service;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Test
{
    public class AdminTutorUnitTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ITutorRepository> _mockTutorRepo;
        private readonly TutorService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminTutorUnitTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockTutorRepo = new Mock<ITutorRepository>();

            // Setup repositories in unit of work mock
            _mockUnitOfWork.SetupGet(u => u.tutorRepository).Returns(_mockTutorRepo.Object);
            _service = new TutorService(_mockUnitOfWork.Object, _userManager);
        }


        [Fact]
        public async Task GetAllTutorsAsync_WithExistingTutors_ReturnAllTutors() {

            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u=> u.tutorRepository.GetAllAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>())).ReturnsAsync(new List<ApplicationUser> {
                new ApplicationUser { Id = "1", FirstName = "David" },
                new ApplicationUser { Id = "2", FirstName = "Jhon" }
            });
            var service = new TutorService(mockUnitOfWork.Object, _userManager);

            //act
            var results = await service.GetAllTutorsAsync();

            //assert
            Assert.NotNull(service);
            Assert.Equal(2, results.Count());
            Assert.Equal("David", results.First().FirstName);





        }

     

        [Fact]
        public async Task GetTutorByIdAsync_ShouldReturnTutor_WhenExists()
        {
            // Arrange
            var tutorId = "tutor1";
            var tutorList = new List<ApplicationUser>
            {
                new ApplicationUser { Id = tutorId, FirstName = "John" }
            };

            _mockTutorRepo.Setup(r => r.GetAllAsync(
                It.IsAny<Expression<Func<ApplicationUser, bool>>>(),
                It.IsAny<string>()))
                .ReturnsAsync(tutorList);

            // Act
            var result = await _service.GetTutorByIdAsync(tutorId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tutorId, result.Id);
        }



        [Fact]
        public async Task SearchTutorsAsync_ShouldReturnFilteredTutors_WhenSearchTermProvided()
        {
            // Arrange
            var searchTerm = "john";
            var tutors = new List<ApplicationUser>
            {
                new ApplicationUser { TutorNo = "T001", FirstName = "John", LastName = "Doe", Email = "john@example.com" },
                new ApplicationUser { TutorNo = "T002", FirstName = "Jane", LastName = "Smith", Email = "jane@example.com" }
            };

            _mockTutorRepo.Setup(r => r.GetAllAsync(
                It.IsAny<Expression<Func<ApplicationUser, bool>>>(),
                It.IsAny<string>()))
                .ReturnsAsync(tutors.Where(t =>
                    (t.FirstName != null && t.FirstName.ToLower().Contains(searchTerm)) ||
                    (t.LastName != null && t.LastName.ToLower().Contains(searchTerm)) ||
                    (t.Email != null && t.Email.ToLower().Contains(searchTerm))
                ));

            // Act
            var result = await _service.SearchTutorsAsync(searchTerm);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Contains(result, t => t.FirstName.ToLower().Contains(searchTerm));
        }

    }
}
