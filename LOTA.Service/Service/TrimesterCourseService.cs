using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service.IService;

namespace LOTA.Service.Service
{
    public class TrimesterCourseService : ITrimesterCourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrimesterCourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TrimesterCourseReturnDTO>> GetAllTrimesterCoursesAsync()
        {
            var trimesterCourses = await _unitOfWork.trimesterCourseRepository.GetAllTrimesterCoursesAsync();
            return trimesterCourses.Select(MapToReturnDTO);
        }

        public async Task<TrimesterCourseReturnDTO> GetTrimesterCourseByIdAsync(string id)
        {
            var trimesterCourse = await _unitOfWork.trimesterCourseRepository.GetTrimesterCourseByIdAsync(id);
            return trimesterCourse != null ? MapToReturnDTO(trimesterCourse) : null;
        }

        public async Task<TrimesterCourseReturnDTO> CreateTrimesterCourseAsync(TrimesterCourseCreateDTO trimesterCourseDto)
        {
            // Check if trimester course already exists
            if (await IsTrimesterCourseExistsAsync(trimesterCourseDto.TrimesterId, trimesterCourseDto.CourseId))
            {
                throw new InvalidOperationException("A course offering for this trimester and course combination already exists.");
            }

            var trimesterCourse = new TrimesterCourse
            {
                Id = Guid.NewGuid().ToString(),
                TrimesterId = trimesterCourseDto.TrimesterId,
                CourseId = trimesterCourseDto.CourseId,
                TutorId = trimesterCourseDto.TutorId,
                IsActive = trimesterCourseDto.IsActive,
                RegistrationDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            var result = await _unitOfWork.trimesterCourseRepository.CreateTrimesterCourseAsync(trimesterCourse);
            await _unitOfWork.SaveAsync();

            return MapToReturnDTO(result);
        }

        public async Task<TrimesterCourseReturnDTO> UpdateTrimesterCourseAsync(TrimesterCourseUpdateDTO trimesterCourseDto)
        {
            var existingTrimesterCourse = await _unitOfWork.trimesterCourseRepository.GetTrimesterCourseByIdAsync(trimesterCourseDto.Id);
            if (existingTrimesterCourse == null)
            {
                throw new InvalidOperationException("Trimester course not found.");
            }
            // Check if the new combination already exists (excluding current record)
            var existingWithNewCombination = await _unitOfWork.trimesterCourseRepository
                .GetTrimesterCoursesByTrimesterAndCourseAsync(trimesterCourseDto.TrimesterId, trimesterCourseDto.CourseId);
            
            if (existingWithNewCombination.Any(tc => tc.Id != trimesterCourseDto.Id))
            {
                throw new InvalidOperationException("A course offering for this trimester and course combination already exists.");
            }


            existingTrimesterCourse.TrimesterId = trimesterCourseDto.TrimesterId;
            existingTrimesterCourse.CourseId = trimesterCourseDto.CourseId;
            existingTrimesterCourse.TutorId = trimesterCourseDto.TutorId;
            existingTrimesterCourse.IsActive = trimesterCourseDto.IsActive;
            existingTrimesterCourse.UpdatedDate = DateTime.UtcNow;

            var result = await _unitOfWork.trimesterCourseRepository.UpdateTrimesterCourseAsync(existingTrimesterCourse);
            await _unitOfWork.SaveAsync();

            return MapToReturnDTO(result);
        }

        public async Task<bool> DeleteTrimesterCourseAsync(string id)
        {
            var result = await _unitOfWork.trimesterCourseRepository.DeleteTrimesterCourseAsync(id);
            if (result)
            {
                await _unitOfWork.SaveAsync();
            }
            return result;
        }

        public async Task<IEnumerable<TrimesterCourseReturnDTO>> GetTrimesterCoursesByTrimesterAsync(string trimesterId)
        {
            var trimesterCourses = await _unitOfWork.trimesterCourseRepository.GetTrimesterCoursesByTrimesterAsync(trimesterId);
            return trimesterCourses.Select(MapToReturnDTO);
        }

        public async Task<IEnumerable<TrimesterCourseReturnDTO>> GetTrimesterCoursesByCourseAsync(string courseId)
        {
            var trimesterCourses = await _unitOfWork.trimesterCourseRepository.GetTrimesterCoursesByCourseAsync(courseId);
            return trimesterCourses.Select(MapToReturnDTO);
        }

        public async Task<IEnumerable<TrimesterCourseReturnDTO>> GetTrimesterCoursesByTutorAsync(string tutorId)
        {
            var trimesterCourses = await _unitOfWork.trimesterCourseRepository.GetTrimesterCoursesByTutorAsync(tutorId);
            return trimesterCourses.Select(MapToReturnDTO);
        }

        public async Task<bool> IsTrimesterCourseExistsAsync(string trimesterId, string courseId)
        {
            return await _unitOfWork.trimesterCourseRepository.IsTrimesterCourseExistsAsync(trimesterId, courseId);
        }

        public async Task<IEnumerable<CourseOfferingReturnDTO>> GetLatestTrimesterCourseOfferingsAsync()
        {
            var trimesterCourses = await _unitOfWork.trimesterCourseRepository.GetLatestTrimesterCourseOfferingsAsync();
            return trimesterCourses.Select(MapToCourseOfferingDTO);
        }

        private TrimesterCourseReturnDTO MapToReturnDTO(TrimesterCourse trimesterCourse)
        {
            return new TrimesterCourseReturnDTO
            {
                Id = trimesterCourse.Id,
                TrimesterId = trimesterCourse.TrimesterId,
                CourseId = trimesterCourse.CourseId,
                TutorId = trimesterCourse.TutorId,
                IsActive = trimesterCourse.IsActive,
                RegistrationDate = trimesterCourse.RegistrationDate,
                CreatedDate = trimesterCourse.CreatedDate,
                UpdatedDate = trimesterCourse.UpdatedDate,
                Trimester = trimesterCourse.Trimester != null ? new TrimesterReturnDTO
                {
                    Id = trimesterCourse.Trimester.Id,
                    AcademicYear = trimesterCourse.Trimester.AcademicYear,
                    TrimesterNumber = trimesterCourse.Trimester.TrimesterNumber,
                    IsActive = trimesterCourse.Trimester.IsActive,
                    CreatedDate = trimesterCourse.Trimester.CreatedDate,
                    UpdatedDate = trimesterCourse.Trimester.UpdatedDate
                } : null,
                Course = trimesterCourse.Course != null ? new CourseReturnDTO
                {
                    Id = trimesterCourse.Course.Id,
                    CourseCode = trimesterCourse.Course.CourseCode,
                    CourseName = trimesterCourse.Course.CourseName,
                    Description = trimesterCourse.Course.Description,
                    IsActive = trimesterCourse.Course.IsActive
                } : null,
                Tutor = trimesterCourse.Tutor != null ? new UserReturnDTO
                {
                    Id = trimesterCourse.Tutor.Id,
                    FirstName = trimesterCourse.Tutor.FirstName,
                    LastName = trimesterCourse.Tutor.LastName,
                    Email = trimesterCourse.Tutor.Email
                } : null
            };
        }

        private CourseOfferingReturnDTO MapToCourseOfferingDTO(TrimesterCourse trimesterCourse)
        {
            return new CourseOfferingReturnDTO
            {
                Id = trimesterCourse.Id,
                CourseId = trimesterCourse.CourseId,
                CourseName = trimesterCourse.Course?.CourseName ?? "",
                CourseCode = trimesterCourse.Course?.CourseCode ?? "",
                Description = trimesterCourse.Course?.Description ?? "",
                Trimester = trimesterCourse.Trimester,
                TutorId = trimesterCourse.TutorId ?? "",
                TutorName = trimesterCourse.Tutor != null ? $"{trimesterCourse.Tutor.FirstName} {trimesterCourse.Tutor.LastName}" : "",
                IsActive = trimesterCourse.IsActive ?? false,
                CreatedDate = trimesterCourse.CreatedDate,
                UpdatedDate = trimesterCourse.UpdatedDate
            };
        }
    }
}
