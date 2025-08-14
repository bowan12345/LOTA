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

        public async Task<TrimesterReturnDTO> GetCurrentTrimesterAsync()
        {
            
            var currentDate = DateTime.Now;
            var currentYear = currentDate.Year;
            var currentMonth = currentDate.Month;

            // Simple logic to determine current trimester based on month
            int currentTrimester;
            if (currentMonth >= 2 && currentMonth <= 6) // Feb to June
                currentTrimester = 1;
            else if (currentMonth >= 7 && currentMonth <= 10) // July to Oct
                currentTrimester = 2;
            else // Oct to Jan
                currentTrimester = 3;

            var trimesters = await _unitOfWork.trimesterRepository.GetActiveTrimestersAsync();
            Trimester? trimester = trimesters.FirstOrDefault(t => t.AcademicYear == currentYear && t.TrimesterNumber == currentTrimester);
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
            //throw new NotImplementedException("TDD Red phase: method not implemented yet");
            var trimester = await _unitOfWork.trimesterRepository.GetByIdAsync(id);
            if (trimester != null)
            {
                //delete trimester data and  automatelly delete related course and student under trimester
                _unitOfWork.trimesterRepository.Remove(trimester.Id);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task DeleteAllAsync(IEnumerable<string> ids)
        {
            //check if empty
            if (ids == null || !ids.Any())
            {
                throw new InvalidOperationException("Trimester already exists.");
            }
            //delete trimester data and  automatelly delete related course and student under trimester
            _unitOfWork.trimesterRepository.RemoveRange(ids);
            await _unitOfWork.SaveAsync();
        }
    }
}
