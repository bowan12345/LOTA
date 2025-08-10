using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Service.Service.IService;

namespace LOTA.Service.Service
{
    public class TrimesterService : ITrimesterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrimesterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Trimester>> GetActiveTrimestersAsync()
        {
            return await _unitOfWork.trimesterRepository.GetActiveTrimestersAsync();
        }

        public async Task<Trimester> GetByAcademicYearAndTrimesterAsync(string academicYear, string trimesterNumber)
        {
            return await _unitOfWork.trimesterRepository.GetByAcademicYearAndTrimesterAsync(academicYear, trimesterNumber);
        }

        public async Task<Trimester> GetCurrentTrimesterAsync()
        {
            var currentDate = DateTime.Now;
            var currentYear = currentDate.Year;
            var currentMonth = currentDate.Month;
            
            // Determine academic year and trimester based on current date
            string academicYear;
            string trimesterNumber;
            
            if (currentMonth >= 2 && currentMonth <= 6)
            {
                // February to June - First Trimester (previous year to current year)
                academicYear = $"{currentYear}";
                trimesterNumber = "tri1";
            }
            else if (currentMonth >= 7 && currentMonth <= 10)
            {
                // July to October - Second Trimester (current year to next year)
                academicYear = $"{currentYear}";
                trimesterNumber = "tri2";
            }
            else
            {
                // November to January - Third Trimester (current year to next year)
                academicYear = $"{currentYear}";
                trimesterNumber = "tri3";
            }
            
            // Try to get the current trimester from database
            var currentTrimester = await _unitOfWork.trimesterRepository.GetByAcademicYearAndTrimesterAsync(academicYear, trimesterNumber);
            
            // If not found, create a default one or return null
            if (currentTrimester == null)
            {
                // Create a default current trimester
                currentTrimester = new Trimester
                {
                    Id = Guid.NewGuid().ToString(),
                    AcademicYear = academicYear,
                    TrimesterNumber = trimesterNumber,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                };
            }
            
            return currentTrimester;
        }
    }
}
