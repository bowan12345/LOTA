using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Service.Service
{
    public class QualificationService : IQualificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QualificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<QualificationReturnDTO>> GetAllQualificationsAsync()
        {
            var qualifications = await _unitOfWork.qualificationRepository.GetAllQualificationsAsync();
            
            return qualifications.Select(q => new QualificationReturnDTO
            {
                Id = q.Id,
                QualificationName = q.QualificationName,
                QualificationType = q.QualificationType?.QualificationTypeName ?? string.Empty,
                IsActive = q.IsActive,
                CreatedDate = q.CreatedDate,
                UpdatedDate = q.UpdatedDate,
                Level = q.Level,
            });
        }

        public async Task<QualificationReturnDTO?> GetQualificationByIdAsync(string id)
        {
            var qualification = await _unitOfWork.qualificationRepository.GetQualificationByIdAsync(id);
            
            if (qualification == null)
                return null;

            return new QualificationReturnDTO
            {
                Id = qualification.Id,
                QualificationName = qualification.QualificationName,
                QualificationType = qualification.QualificationType?.QualificationTypeName ?? string.Empty,
                IsActive = qualification.IsActive,
                CreatedDate = qualification.CreatedDate,
                UpdatedDate = qualification.UpdatedDate,
                Level = qualification.Level,
            };
        }

        public async Task<QualificationReturnDTO> CreateQualificationAsync(QualificationCreateDTO qualificationDto)
        {
            // Check if qualification name already exists
            if (await _unitOfWork.qualificationRepository.IsQualificationNameExistsAsync(qualificationDto.QualificationName))
            {
                throw new InvalidOperationException($"Qualification with name '{qualificationDto.QualificationName}' already exists.");
            }

            // Find the QualificationType by name
            var qualificationType = await _unitOfWork.qualificationRepository.GetQualificationTypeByTypeNameAsync(qualificationDto.QualificationType);
            if (qualificationType == null)
            {
                throw new InvalidOperationException($"Qualification type '{qualificationDto.QualificationType}' not found.");
            }

            var qualification = new Qualification
            {
                Id = Guid.NewGuid().ToString(),
                QualificationName = qualificationDto.QualificationName,
                QualificationTypeId = qualificationType.Id,
                Level = qualificationDto.Level,
                IsActive = qualificationDto.IsActive,
                CreatedDate = DateTime.UtcNow,
                Courses = new List<Course>()
            };

            await _unitOfWork.qualificationRepository.AddAsync(qualification);
            await _unitOfWork.SaveAsync();

            return new QualificationReturnDTO
            {
                Id = qualification.Id,
                QualificationName = qualification.QualificationName,
                QualificationType = qualificationType.QualificationTypeName,
                IsActive = qualification.IsActive,
                CreatedDate = qualification.CreatedDate,
                UpdatedDate = qualification.UpdatedDate,
                Level = qualification.Level
            };
        }


        public async Task<QualificationReturnDTO> UpdateQualificationAsync(QualificationUpdateDTO qualificationDto)
        {
            var existingQualification = await _unitOfWork.qualificationRepository.GetQualificationByIdAsync(qualificationDto.Id);
            
            if (existingQualification == null)
            {
                throw new InvalidOperationException($"Qualification with ID '{qualificationDto.Id}' not found.");
            }

            // Check if qualification name already exists (excluding current qualification)
            if (await _unitOfWork.qualificationRepository.IsQualificationNameExistsAsync(qualificationDto.QualificationName, qualificationDto.Id))
            {
                throw new InvalidOperationException($"Qualification with name '{qualificationDto.QualificationName}' already exists.");
            }

            // Find the QualificationType by name
            var qualificationType = await _unitOfWork.qualificationRepository.GetQualificationTypeByTypeNameAsync(qualificationDto.QualificationType);
            if (qualificationType == null)
            {
                throw new InvalidOperationException($"Qualification type '{qualificationDto.QualificationType}' not found.");
            }

            existingQualification.QualificationName = qualificationDto.QualificationName;
            existingQualification.QualificationTypeId = qualificationType.Id;
            existingQualification.Level = qualificationDto.Level;
            existingQualification.IsActive = qualificationDto.IsActive;
            existingQualification.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.qualificationRepository.Update(existingQualification);
            await _unitOfWork.SaveAsync();

            return new QualificationReturnDTO
            {
                Id = existingQualification.Id,
                QualificationName = existingQualification.QualificationName,
                QualificationType = qualificationType.QualificationTypeName,
                IsActive = existingQualification.IsActive,
                CreatedDate = existingQualification.CreatedDate,
                UpdatedDate = existingQualification.UpdatedDate,
                Level = existingQualification.Level
            };
        }

        public async Task<bool> DeleteQualificationAsync(string id)
        {
            var qualification = await _unitOfWork.qualificationRepository.GetQualificationByIdAsync(id);
            
            if (qualification == null)
            {
                return false;
            }

            // Check if qualification has associated courses
            if (qualification.Courses != null && qualification.Courses.Any())
            {
                throw new InvalidOperationException($"Cannot delete qualification '{qualification.QualificationName}' because it has associated courses.");
            }

            _unitOfWork.qualificationRepository.Remove(qualification);
            await _unitOfWork.SaveAsync();
            
            return true;
        }

        public async Task<bool> IsQualificationNameExistsAsync(string name, string? excludeId = null)
        {
            return await _unitOfWork.qualificationRepository.IsQualificationNameExistsAsync(name, excludeId);
        }

        public async Task<IEnumerable<string>> GetAllQualificationTypesAsync()
        {
            var qualificationTypes = await _unitOfWork.qualificationRepository.GetAllQualificationTypesAsync();
            return qualificationTypes;
        }
    }
}
