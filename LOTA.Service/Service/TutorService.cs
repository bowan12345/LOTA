using LOTA.DataAccess.Repository;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Service.Service.IService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Service.Service
{
    public class TutorService : ITutorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TutorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApplicationUser> GetTutorByEmailAsync(string email)
        {
            throw new NotImplementedException("TDD Red phase: method not implemented yet");
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllTutorsAsync()
        {
            // Get only users who have TutorNo (i.e., are tutors) and include their courses
            return await _unitOfWork.tutorRepository.GetAllAsync(
                filter: u => !string.IsNullOrEmpty(u.TutorNo),
                includeProperties: "TutorCourse.Course"
            );
        }

        public async Task AddTutorCourseAsync(string TutorId, List<string> AssignedCourses)
        {
            
            List<TutorCourse> tutorCourses = new List<TutorCourse>();
            foreach (string course in AssignedCourses)
            {
                TutorCourse tutorCourse = new TutorCourse()
                {
                    // Generate unique ID
                    Id = Guid.NewGuid().ToString(), 
                    TutorId = TutorId,
                    CourseId = course
                };
                tutorCourses.Add(tutorCourse);
            }
            await _unitOfWork.tutorCourseRepository.AddRangeAsync(tutorCourses);
            await _unitOfWork.SaveAsync(); 
        }

        public async Task<ApplicationUser> GetTutorByIdAsync(string id)
        {
          
            var tutors = await _unitOfWork.tutorRepository.GetAllAsync(
                filter: u => u.Id == id,
                includeProperties: "TutorCourse.Course"
            );
            return tutors.FirstOrDefault();
        }

        public async Task<IEnumerable<TutorCourse>> GetTutorCoursesAsync(string tutorId)
        {
            
            return await _unitOfWork.tutorCourseRepository.GetAllAsync(
                filter: tc => tc.TutorId == tutorId,
                includeProperties: "Course"
            );
        }

        public async Task RemoveAllTutorCoursesAsync(string tutorId)
        {
            
            var existingCourses = await _unitOfWork.tutorCourseRepository.GetAllAsync(
                filter: tc => tc.TutorId == tutorId
            );
            
            if (existingCourses.Any())
            {
                _unitOfWork.tutorCourseRepository.RemoveRange(existingCourses.Select(co=>co.Id));
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<IEnumerable<ApplicationUser>> SearchTutorsAsync(string searchTerm)
        {
           
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                // If search term is empty, return all tutors
                return await GetAllTutorsAsync();
            }

            // Convert search term to lowercase for case-insensitive search
            searchTerm = searchTerm.ToLower();

            // Get tutors with search filter
            return await _unitOfWork.tutorRepository.GetAllAsync(
                filter: u => !string.IsNullOrEmpty(u.TutorNo) && 
                            (u.FirstName != null && u.FirstName.ToLower().Contains(searchTerm) ||
                             u.LastName != null && u.LastName.ToLower().Contains(searchTerm) ||
                             u.Email != null && u.Email.ToLower().Contains(searchTerm)),
                includeProperties: "TutorCourse.Course"
            );
        }
          
    }
}
