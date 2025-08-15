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
    public class AdminQualificationUnitTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IQualificationRepository> _qualificationRepoMock;
        private readonly QualificationService _service;

        public AdminQualificationUnitTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _qualificationRepoMock = new Mock<IQualificationRepository>();
            _unitOfWorkMock.SetupGet(u => u.qualificationRepository).Returns(_qualificationRepoMock.Object);

            _service = new QualificationService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetAllQualificationsAsync_ShouldReturnMappedDTOs()
        {
            // Arrange
            var qualifications = new List<Qualification>
            {
                new Qualification
                {
                    Id = "1",
                    QualificationName = "Q1",
                    QualificationType = new QualificationType { QualificationTypeName = "TypeA" },
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = null,
                    Level = 1
                }
            };

            _qualificationRepoMock
                .Setup(r => r.GetAllQualificationsAsync())
                .ReturnsAsync(qualifications);

            // Act
            var result = await _service.GetAllQualificationsAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Q1", result.First().QualificationName);
            Assert.Equal("TypeA", result.First().QualificationType);
        }

        [Fact]
        public async Task GetQualificationByIdAsync_ShouldReturnQualificationDTO()
        {
            // Arrange
            var qualification = new Qualification
            {
                Id = "1",
                QualificationName = "Q1",
                QualificationType = new QualificationType { QualificationTypeName = "TypeA" },
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = null,
                Level = 1
            };

            _qualificationRepoMock
                .Setup(r => r.GetQualificationByIdAsync("1"))
                .ReturnsAsync(qualification);

            // Act
            var result = await _service.GetQualificationByIdAsync("1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Q1", result.QualificationName);
            Assert.Equal("TypeA", result.QualificationType);
        }

        [Fact]
        public async Task CreateQualificationAsync_ShouldAddAndReturnDTO()
        {
            // Arrange
            var createDto = new QualificationCreateDTO
            {
                QualificationName = "Q1",
                QualificationType = "TypeA",
                Level = 1,
                IsActive = true
            };

            var qualificationType = new QualificationType
            {
                Id = "type1",
                QualificationTypeName = "TypeA"
            };

            _qualificationRepoMock
                .Setup(r => r.IsQualificationNameExistsAsync("Q1", null))
                .ReturnsAsync(false);

            _qualificationRepoMock
                .Setup(r => r.GetQualificationTypeByTypeNameAsync("TypeA"))
                .ReturnsAsync(qualificationType);

            _qualificationRepoMock
                .Setup(r => r.AddAsync(It.IsAny<Qualification>()))
                .ReturnsAsync(new Qualification());

            _unitOfWorkMock
                .Setup(u => u.SaveAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _service.CreateQualificationAsync(createDto);

            // Assert
            Assert.Equal("Q1", result.QualificationName);
            Assert.Equal("TypeA", result.QualificationType);
        }

        [Fact]
        public async Task UpdateQualificationAsync_ShouldUpdateAndReturnDTO()
        {
            // Arrange
            var updateDto = new QualificationUpdateDTO
            {
                Id = "1",
                QualificationName = "Q1 updated",
                QualificationType = "TypeB",
                Level = 2
            };

            var existingQualification = new Qualification
            {
                Id = "1",
                QualificationName = "Q1",
                QualificationTypeId = "type1",
                QualificationType = new QualificationType { QualificationTypeName = "TypeA" },
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            var qualificationType = new QualificationType
            {
                Id = "type2",
                QualificationTypeName = "TypeB"
            };

            _qualificationRepoMock
                .Setup(r => r.GetQualificationByIdAsync("1"))
                .ReturnsAsync(existingQualification);

            _qualificationRepoMock
                .Setup(r => r.IsQualificationNameExistsAsync("Q1 updated", "1"))
                .ReturnsAsync(false);

            _qualificationRepoMock
                .Setup(r => r.GetQualificationTypeByTypeNameAsync("TypeB"))
                .ReturnsAsync(qualificationType);

            _qualificationRepoMock
                .Setup(r => r.Update(existingQualification));

            _unitOfWorkMock
                .Setup(u => u.SaveAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _service.UpdateQualificationAsync(updateDto);

            // Assert
            Assert.Equal("Q1 updated", result.QualificationName);
            Assert.Equal("TypeB", result.QualificationType);
        }

        [Fact]
        public async Task DeleteQualificationAsync_ShouldRemoveAndReturnTrue()
        {
            // Arrange
            var qualification = new Qualification
            {
                Id = "1",
                QualificationName = "Q1",
                Courses = new List<Course>()
            };

            _qualificationRepoMock
                .Setup(r => r.GetQualificationByIdAsync("1"))
                .ReturnsAsync(qualification);

            _qualificationRepoMock
                .Setup(r => r.Remove(qualification.Id));

            _unitOfWorkMock
                .Setup(u => u.SaveAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _service.DeleteQualificationAsync("1");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsQualificationNameExistsAsync_ShouldReturnTrue()
        {
            // Arrange
            _qualificationRepoMock
                .Setup(r => r.IsQualificationNameExistsAsync("Q1", null))
                .ReturnsAsync(true);

            // Act
            var result = await _service.IsQualificationNameExistsAsync("Q1");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAllQualificationTypesAsync_ShouldReturnListOfStrings()
        {
            // Arrange
            var types = new List<string> { "TypeA", "TypeB" };

            _qualificationRepoMock
                .Setup(r => r.GetAllQualificationTypesAsync())
                .ReturnsAsync(types);

            // Act
            var result = await _service.GetAllQualificationTypesAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains("TypeA", result);
            Assert.Contains("TypeB", result);
        }

    }
}
