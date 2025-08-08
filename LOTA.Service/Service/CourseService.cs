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


        public async Task<IEnumerable<CourseReturnDTO>> GetAllCoursesAsync()
        {
            //throw new NotImplementedException("TDD Red phase: method not implemented yet");
            // get all courses based on filter conditions
            IEnumerable<Course> courses = await _unitOfWork.courseRepository.GetAllAsync(includeProperties: "LearningOutcomes");
            return courses.Select(MapToDTO);

        }

        public async Task<IEnumerable<CourseReturnDTO>> GetCoursesByNameOrCodeAsync(string courseSearchItem)
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
            IEnumerable<Course> courses = await _unitOfWork.courseRepository.GetAllAsync(filter,includeProperties: "LearningOutcomes");
            return courses.Select(MapToDTO);
        }

        public async Task RemoveCourse(string courseId)
        {
            //throw new NotImplementedException();
            if (string.IsNullOrEmpty(courseId))
            {
                throw new NullReferenceException("courseId is empty");
            }
            //remove course
            var course = new Course() { Id = courseId };
            try
            {
                _unitOfWork.courseRepository.Remove(course);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
           

        public async Task UpdateCourse(Course course)
        {
            //throw new NotImplementedException();
            try
            {
                if (course == null)
                {
                    throw new NullReferenceException("course is empty");
                }
                _unitOfWork.courseRepository.Update(course);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }



        private CourseReturnDTO MapToDTO(Course course)
        {
            return new CourseReturnDTO
            {
                Id = course.Id,
                CourseCode = course.CourseCode,
                CourseName = course.CourseName,
                Description = course.Description,
                IsActive = course.IsActive,
                CreatedDate = course.CreatedDate,
                UpdatedDate = course.UpdatedDate,
                LearningOutcomes = course.LearningOutcomes?.Select(lo => new LearningOutcomeDTO
                {
                    Id = lo.Id,
                    LOName = lo.LOName,
                    Description = lo.Description,
                    MaxScore = lo.MaxScore,
                    Weight = lo.Weight,
                    CreatedDate = lo.CreatedDate,
                    UpdatedDate = lo.UpdatedDate
                }).ToList() ?? new List<LearningOutcomeDTO>()
            };
        }
    }
}
