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
    public class AdminTrimesterUnitTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ITrimesterRepository> _trimesterRepoMock;
        private readonly TrimesterService _service;

        public AdminTrimesterUnitTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _trimesterRepoMock = new Mock<ITrimesterRepository>();
            _unitOfWorkMock.Setup(u => u.trimesterRepository).Returns(_trimesterRepoMock.Object);

            _service = new TrimesterService(_unitOfWorkMock.Object);
        }


        [Fact]
        public async Task GetActiveTrimestersAsync_ShouldReturnDTOList()
        {
            // Arrange
            var trimesters = new List<Trimester>
            {
                new Trimester { Id = "1", AcademicYear = 2025, TrimesterNumber = 1, IsActive = true }
            };
            _trimesterRepoMock.Setup(r => r.GetActiveTrimestersAsync())
                .ReturnsAsync(trimesters);

            // Act
            var result = await _service.GetActiveTrimestersAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("1", result.First().Id);
        }

        [Fact]
        public async Task GetByAcademicYearAsync_ShouldReturnDTOList()
        {
            var trimesters = new List<Trimester>
            {
                new Trimester { Id = "2", AcademicYear = 2025, TrimesterNumber = 2, IsActive = true }
            };
            _trimesterRepoMock.Setup(r => r.GetByAcademicYearAsync(2025))
                .ReturnsAsync(trimesters);

            var result = await _service.GetByAcademicYearAsync(2025);

            Assert.Single(result);
            Assert.Equal(2025, result.First().AcademicYear);
        }

        [Fact]
        public async Task GetByAcademicYearAndTrimesterAsync_ShouldReturnDTO()
        {
            var trimester = new Trimester { Id = "3", AcademicYear = 2025, TrimesterNumber = 3, IsActive = true };
            _trimesterRepoMock.Setup(r => r.GetByAcademicYearAndTrimesterAsync(2025, 3))
                .ReturnsAsync(trimester);

            var result = await _service.GetByAcademicYearAndTrimesterAsync(2025, 3);

            Assert.Equal("3", result.Id);
            Assert.Equal(3, result.TrimesterNumber);
        }

        [Fact]
        public async Task GetCurrentTrimesterAsync_ShouldReturnCurrentTrimesterDTO()
        {
            var now = DateTime.Now;
            var trimester = new Trimester
            {
                Id = "4",
                AcademicYear = now.Year,
                TrimesterNumber = now.Month >= 2 && now.Month <= 6 ? 1 :
                                  now.Month >= 7 && now.Month <= 10 ? 2 : 3,
                IsActive = true
            };
            _trimesterRepoMock.Setup(r => r.GetActiveTrimestersAsync())
                .ReturnsAsync(new List<Trimester> { trimester });

            var result = await _service.GetCurrentTrimesterAsync();

            Assert.Equal("4", result.Id);
            Assert.Equal(trimester.TrimesterNumber, result.TrimesterNumber);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnDTO()
        {
            var trimester = new Trimester { Id = "5", AcademicYear = 2025, TrimesterNumber = 1 };
            _trimesterRepoMock.Setup(r => r.GetByIdAsync("5", It.IsAny<string>()))
                .ReturnsAsync(trimester);

            var result = await _service.GetByIdAsync("5");

            Assert.Equal("5", result.Id);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedTrimesterDTO()
        {
            var dto = new TrimesterCreateDTO { AcademicYear = 2025, TrimesterNumber = 2 };
            _trimesterRepoMock.Setup(r => r.GetByAcademicYearAndTrimesterAsync(2025, 2))
                .ReturnsAsync((Trimester)null);

            Trimester savedTrimester = null;
            _trimesterRepoMock.Setup(r => r.AddAsync(It.IsAny<Trimester>()))
                .Callback<Trimester>(t => savedTrimester = t)
                .ReturnsAsync(new Trimester());

            _unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(1);

            var result = await _service.CreateAsync(dto);

            Assert.NotNull(savedTrimester);
            Assert.Equal(dto.AcademicYear, result.AcademicYear);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedTrimesterDTO()
        {
            var dto = new TrimesterUpdateDTO { Id = "6", AcademicYear = 2025, TrimesterNumber = 2 };
            var trimester = new Trimester { Id = "6", AcademicYear = 2024, TrimesterNumber = 1 };

            _trimesterRepoMock.Setup(r => r.GetByIdAsync("6", It.IsAny<string>())).ReturnsAsync(trimester);
            _trimesterRepoMock.Setup(r => r.GetByAcademicYearAndTrimesterAsync(2025, 2))
                .ReturnsAsync((Trimester)null);
            _unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(1);

            var result = await _service.UpdateAsync(dto);

            Assert.Equal(2025, result.AcademicYear);
            Assert.Equal(2, result.TrimesterNumber);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveTrimester()
        {
            var trimester = new Trimester { Id = "7" };
            _trimesterRepoMock.Setup(r => r.GetByIdAsync("7", It.IsAny<string>())).ReturnsAsync(trimester);
            _unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(1);

            await _service.DeleteAsync("7");

            _trimesterRepoMock.Verify(r => r.Remove(trimester), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }
    }
}
