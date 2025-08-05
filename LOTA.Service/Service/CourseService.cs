using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Service.Service
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            await _unitOfWork.courseRepo.AddAsync(course);
            await _unitOfWork.SaveAsync();  
            return course;
        }

        /*public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            throw new NotImplementedException("TDD Red phase: method not implemented yet");
        }*/

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            IEnumerable<Course> courses = await _unitOfWork.courseRepo.GetAllAsync();
            return courses;

        }
    }
}
