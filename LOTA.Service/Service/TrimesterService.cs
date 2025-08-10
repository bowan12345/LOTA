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

        public async Task<Trimester> GetByAcademicYearAndTrimesterAsync(int academicYear, int trimesterNumber)
        {
            return await _unitOfWork.trimesterRepository.GetByAcademicYearAndTrimesterAsync(academicYear, trimesterNumber);
        }

        public async Task<Trimester> GetCurrentTrimesterAsync()
        {

            var currentDate = DateTime.Now;
            var currentYear = currentDate.Year;
            var currentMonth = currentDate.Month;
            

            IEnumerable<Trimester> enumerable = await _unitOfWork.trimesterRepository.GetActiveTrimestersAsync();
            return null;
        }
    }
}
