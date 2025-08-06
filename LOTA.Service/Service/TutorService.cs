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
            //throw new NotImplementedException();
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
          
    }
}
