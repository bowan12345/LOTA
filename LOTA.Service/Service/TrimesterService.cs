using Azure.Core;
using LOTA.DataAccess.Repository;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service.IService;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LOTA.Service.Service
{
    public class TrimesterService : ITrimesterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrimesterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TrimesterReturnDTO>> GetActiveTrimestersAsync()
        {
            //throw new NotImplementedException("TDD Red phase: method not implemented yet");
            IEnumerable<Trimester> List = await _unitOfWork.trimesterRepository.GetActiveTrimestersAsync();
            var returnDTOList = List.Select(trimester => new TrimesterReturnDTO
            {
                Id = trimester.Id,
                AcademicYear = trimester.AcademicYear,
                TrimesterNumber = trimester.TrimesterNumber,
                IsActive = trimester.IsActive,
                CreatedDate = trimester.CreatedDate,
                UpdatedDate = trimester.UpdatedDate,
            });
            return returnDTOList;
        }

        public async Task<IEnumerable<TrimesterReturnDTO>> GetByAcademicYearAsync(int academicYear)
        {
            //throw new NotImplementedException("TDD Red phase: method not implemented yet");
            IEnumerable<Trimester> trimesterList = await _unitOfWork.trimesterRepository.GetByAcademicYearAsync(academicYear);
            var returnListDTO = trimesterList.Select(trimester => new TrimesterReturnDTO
            {
                Id = trimester.Id,
                AcademicYear = trimester.AcademicYear,
                TrimesterNumber = trimester.TrimesterNumber,
                IsActive = trimester.IsActive,
                CreatedDate = trimester.CreatedDate,
                UpdatedDate = trimester.UpdatedDate,
            });
            return returnListDTO;
        }

        public async Task<TrimesterReturnDTO> GetByAcademicYearAndTrimesterAsync(int academicYear, int trimesterNumber)
        {
            
            Trimester trimester = await _unitOfWork.trimesterRepository.GetByAcademicYearAndTrimesterAsync(academicYear, trimesterNumber);
            var returnDTO = new TrimesterReturnDTO()
            {
                Id = trimester.Id,
                AcademicYear = trimester.AcademicYear,
                TrimesterNumber = trimester.TrimesterNumber,
                IsActive = trimester.IsActive,
                CreatedDate = trimester.CreatedDate,
                UpdatedDate = trimester.UpdatedDate,
            };
            return returnDTO;
        }

        public async Task<TrimesterReturnDTO> GetLatestTrimesterAsync()
        {
            
            var trimester = await _unitOfWork.trimesterRepository.GetLatestTrimestersAsync();
            if (trimester == null)
            {
                //return empty object
                return new TrimesterReturnDTO();
            }
            var returnDTO = new TrimesterReturnDTO()
            {
                Id = trimester.Id,
                AcademicYear = trimester.AcademicYear,
                TrimesterNumber = trimester.TrimesterNumber,
                IsActive = trimester.IsActive,
                CreatedDate = trimester.CreatedDate,
                UpdatedDate = trimester.UpdatedDate,
            };
            return returnDTO;
        }

        public async Task<TrimesterReturnDTO> GetByIdAsync(string id)
        {
            
            Trimester trimester = await _unitOfWork.trimesterRepository.GetByIdAsync(id);
            if (trimester == null)
            {
                throw new InvalidOperationException("Trimester doesn't exist.");
            }
            var returnDTO = new TrimesterReturnDTO()
            {
                Id = trimester.Id,
                AcademicYear = trimester.AcademicYear,
                TrimesterNumber = trimester.TrimesterNumber,
                IsActive = trimester.IsActive,
                CreatedDate = trimester.CreatedDate,
                UpdatedDate = trimester.UpdatedDate,
            };
            return returnDTO;
        }

        public async Task<TrimesterReturnDTO> CreateAsync(TrimesterCreateDTO trimesterDTO)
        {
            
            //check if trimester already exists
            var trimesterReturn = await _unitOfWork.trimesterRepository.GetByAcademicYearAndTrimesterAsync(trimesterDTO.AcademicYear, trimesterDTO.TrimesterNumber);
            if (trimesterReturn != null)
            {
                throw new InvalidOperationException("Trimester already exists.");
            }

            //create new trimester object
            var trimester = new Trimester()
            {
                Id = Guid.NewGuid().ToString(),
                AcademicYear = trimesterDTO.AcademicYear,
                TrimesterNumber = trimesterDTO.TrimesterNumber,
                IsActive = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            //save to trimester table
            await _unitOfWork.trimesterRepository.AddAsync(trimester);
            await _unitOfWork.SaveAsync();
            //return trimesterDTO object
            var returnDTO = new TrimesterReturnDTO()
            {
                Id = trimester.Id,
                AcademicYear = trimester.AcademicYear,
                TrimesterNumber = trimester.TrimesterNumber,
                IsActive = trimester.IsActive,
                CreatedDate = trimester.CreatedDate,
                UpdatedDate = trimester.UpdatedDate,
            };
            return returnDTO;
        }

        public async Task<TrimesterReturnDTO> UpdateAsync(TrimesterUpdateDTO trimesterDTO)
        {
            
            // Get trimester by id
            var trimester = await _unitOfWork.trimesterRepository.GetByIdAsync(trimesterDTO.Id);
            if (trimester == null)
            {
                throw new InvalidOperationException($"trimester doesn't exist.");
            }
            //check if trimester already exists (excluding current trimester)
            var existingTrimester = await _unitOfWork.trimesterRepository.GetByAcademicYearAndTrimesterAsync(trimesterDTO.AcademicYear, trimesterDTO.TrimesterNumber);
            if (existingTrimester != null && existingTrimester.Id == trimesterDTO.Id)
            {
                throw new InvalidOperationException("Trimester already exists.");
            }

            // Update trimester properties
            trimester.AcademicYear = trimesterDTO.AcademicYear;
            trimester.TrimesterNumber = trimesterDTO.TrimesterNumber;
            trimester.UpdatedDate = DateTime.Now;

            _unitOfWork.trimesterRepository.Update(trimester);
            await _unitOfWork.SaveAsync();

            // Return updated DTO
            var trimesterReturnDTO = new TrimesterReturnDTO()
            {
                Id = trimester.Id,
                AcademicYear = trimester.AcademicYear,
                TrimesterNumber = trimester.TrimesterNumber,
                IsActive = trimester.IsActive,
                CreatedDate = trimester.CreatedDate,
                UpdatedDate = trimester.UpdatedDate,
            };

            return trimesterReturnDTO;

        }

        public async Task DeleteAsync(string id)
        {
            var trimester = await _unitOfWork.trimesterRepository.GetByIdAsync(id);
            if (trimester == null)
            {
                return;
            }

            // 1) Delete all course offerings under this trimester and their related data
            var offerings = await _unitOfWork.trimesterCourseRepository.GetTrimesterCoursesByTrimesterAsync(id);
            foreach (var offering in offerings)
            {
                var offeringId = offering.Id;

                // 1.1 Delete LO scores (batch by course offering)
                var loScores = await _unitOfWork.studentLOScoreRepository.GetLOScoresByCourseOfferingAsync(offeringId);
                if (loScores != null && loScores.Any())
                {
                    _unitOfWork.studentLOScoreRepository.RemoveRange(loScores.Select(s => s.Id));
                }

                // 1.2 Delete student assessment scores (batch)
                _unitOfWork.studentScoreRepository.RemoveByCourseOfferingId(offeringId);

                // 1.3 Delete assessment-LO mappings, then delete assessments
                var assessments = await _unitOfWork.assessmentRepository.GetAssessmentsByCourseOfferingId(offeringId);
                if (assessments != null && assessments.Any())
                {
                    foreach (var a in assessments)
                    {
                        _unitOfWork.assessmentRepository.RemoveLearningOutcomesByAssessmentIdAsync(a.Id);
                    }
                    _unitOfWork.assessmentRepository.RemoveAssessmentsByCourseOfferingId(offeringId);
                }

                // 1.4 Delete student-course enrollments (batch)
                _unitOfWork.studentCourseRepository.RemoveEnrolledStudentByCourseOfferingId(offeringId);

                // 1.5 Delete the course offering itself
                await _unitOfWork.trimesterCourseRepository.DeleteTrimesterCourseAsync(offeringId);
            }

            // 2) Delete the trimester itself
            _unitOfWork.trimesterRepository.Remove(trimester.Id);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAllAsync(IEnumerable<string> ids)
        {
            //check if empty
            if (ids == null || !ids.Any())
            {
                throw new InvalidOperationException("Trimester already exists.");
            }
            // Delete one by one to ensure cascading cleanup
            foreach (var id in ids)
            {
                await DeleteAsync(id);
            }
        }
    }
}
