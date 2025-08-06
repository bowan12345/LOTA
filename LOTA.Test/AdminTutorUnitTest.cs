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
    public class AdminTutorUnitTest
    {

        [Fact]
        public async Task GetAllTutorsAsync_WithExistingTutors_ReturnAllTutors() {

            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u=> u.tutorRepository.GetAllAsync(null, null)).ReturnsAsync(new List<ApplicationUser> {
                new ApplicationUser { Id = "1", FirstName = "David" },
                new ApplicationUser { Id = "2", FirstName = "Jhon" }
            });
            var service = new TutorService(mockUnitOfWork.Object);

            //act
            var results = await service.GetAllTutorsAsync();

            //assert
            Assert.NotNull(service);
            Assert.Equal(2, results.Count());
            Assert.Equal("David", results.First().FirstName);





        }
    }
}
