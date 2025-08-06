using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Model.DTO;
using LOTA.Service.Service.IService;
using LOTA.Utility;
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
            await _unitOfWork.courseRepository.AddAsync(course);
            await _unitOfWork.SaveAsync();  
            return course;
        }

        /*public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            throw new NotImplementedException("TDD Red phase: method not implemented yet");
        }*/

        public async Task<IEnumerable<Course>> GetAllCoursesAsync(CourseQueryDTO courseQueryDTO)
        {
            // create a combine multipile filter conditions object
            var filter = PredicateBuilder.True<Course>();

            // add filter conditions if any
            if (courseQueryDTO !=null) 
            {
                if (!string.IsNullOrEmpty(courseQueryDTO.CourseName))
                {
                    filter = filter.AndAlso(p => p.CourseName.Contains(courseQueryDTO.CourseName));
                }
                if (!string.IsNullOrEmpty(courseQueryDTO.CourseCode))
                {
                    filter = filter.AndAlso(p => p.CourseCode.Contains(courseQueryDTO.CourseCode));
                }
            }
            // get all courses based on filter conditions
            IEnumerable<Course> courses = await _unitOfWork.courseRepository.GetAllAsync(filter);
            return courses;

        }

        public async Task<IEnumerable<Course>> GetCoursesByNameOrCodeAsync(string courseSearchItem)
        {
            //throw new NotImplementedException();
            // create a combine multipile filter conditions object
            var filter = PredicateBuilder.True<Course>();

            // add filter conditions
            if (!string.IsNullOrEmpty(courseSearchItem))
            {
                filter = PredicateBuilder.False<Course>();
                filter = filter.Or(p => p.CourseName.Contains(courseSearchItem));
                filter = filter.Or(p => p.CourseCode.Contains(courseSearchItem));
            }
            // get all courses based on filter conditions
            IEnumerable<Course> courses = await _unitOfWork.courseRepository.GetAllAsync(filter);
            return courses;
        }
    }
}
