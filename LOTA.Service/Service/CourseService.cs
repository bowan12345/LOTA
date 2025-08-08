using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Model.DTO;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service.IService;
using LOTA.Utility;

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

        public async Task<CourseReturnDTO> CreateCourseAsync(CourseCreateDTO courseDTO)
        {
            // Service layer business logic processing
            
            // Generate new course ID
            var courseId = Guid.NewGuid().ToString();
            
            // Create course entity
            var course = new Course()
            {
                Id = courseId,
                CourseName = courseDTO.CourseName,
                CourseCode = courseDTO.CourseCode,
                Description = courseDTO.Description,
                IsActive = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            
            // Save course to database
            await _unitOfWork.courseRepository.AddAsync(course);
            
            // Process learning outcomes
            if (courseDTO.LearningOutcomes != null && courseDTO.LearningOutcomes.Count > 0)
            {
                // Filter out empty or invalid learning outcomes
                var validLearningOutcomes = courseDTO.LearningOutcomes
                    .Where(lo => !string.IsNullOrWhiteSpace(lo.LOName) && !string.IsNullOrWhiteSpace(lo.Description))
                    .ToList();
                
                if (validLearningOutcomes.Count > 0)
                {
                    var learningOutcomes = validLearningOutcomes.Select(lo => new LearningOutcome()
                    {
                        Id = Guid.NewGuid().ToString(),
                        LOName = lo.LOName,
                        Description = lo.Description,
                        MaxScore = 100, // Default value
                        Weight = 1, // Default value
                        CourseId = courseId, // Set foreign key relationship
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }).ToList();
                
                    // Save learning outcomes to database
                    await _unitOfWork.learningOutcomeRepository.AddRangeAsync(learningOutcomes);
                    await _unitOfWork.SaveAsync();
                }
            }
            
            // Return creation result
            return new CourseReturnDTO()
            {
                Id = courseId,
                CourseCode = courseDTO.CourseCode,
                CourseName = courseDTO.CourseName,
                Description = courseDTO.Description
            };
        }

        public async Task<IEnumerable<CourseReturnDTO>> GetAllCoursesAsync()
        {
            //throw new NotImplementedException("TDD Red phase: method not implemented yet");
            // get all courses based on filter conditions
            IEnumerable<Course> courses = await _unitOfWork.courseRepository.GetAllAsync(includeProperties: "LearningOutcomes");
            return courses.Select(MapToDTO);

        }

        public async Task<CourseReturnDTO> GetCourseByCodeAsync(string courseCode)
        {
            //throw new NotImplementedException();
            if (string.IsNullOrEmpty(courseCode))
            {
                throw new NullReferenceException("courseCode is empty");
            }
            Course course = await _unitOfWork.courseRepository.GetCourseByCodeAsync(courseCode);
            if (course == null)
            {
                return null;
            }
            return MapToDTO(course);
        }

        public async Task<CourseReturnDTO> GetCourseByIdAsync(string courseId)
        {
            if (string.IsNullOrEmpty(courseId))
            {
                throw new NullReferenceException("courseId is empty");
            }
            
            Course course = await _unitOfWork.courseRepository.GetByIdAsync(courseId, includeProperties: "LearningOutcomes");
            if (course == null)
            {
                return null;
            }
            return MapToDTO(course);
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
           

        public async Task UpdateCourse(CourseUpdateDTO courseDTO)
        {
            //throw new NotImplementedException();
            try
            {
                if (courseDTO == null)
                {
                    throw new NullReferenceException("course is empty");
                }
                var course = await _unitOfWork.courseRepository.GetByIdAsync(courseDTO.Id);
                if (course == null)
                {
                    throw new NullReferenceException("course not found");
                }
                course.CourseCode = courseDTO.CourseCode;
                course.CourseName = courseDTO.CourseName;
                course.Description = courseDTO.Description;

                // Get existing learning outcomes
                var existingLearningOutcomes = await _unitOfWork.learningOutcomeRepository.GetAllAsync(lo => lo.CourseId == courseDTO.Id);
               
                // Process learning outcomes update - update existing ones or add new ones
                if (courseDTO.LearningOutcomes != null && courseDTO.LearningOutcomes.Count > 0)
                {
                    // Filter out empty or invalid learning outcomes
                    var validLearningOutcomes = courseDTO.LearningOutcomes
                        .Where(lo => !string.IsNullOrWhiteSpace(lo.LOName) && !string.IsNullOrWhiteSpace(lo.Description))
                        .ToList();

                    if (validLearningOutcomes.Count <= 0)
                    {
                        return;
                    }
                    // Get existing learning outcomes
                    var existingLearningOutcomesDict = existingLearningOutcomes.ToDictionary(lo => lo.Id);
                        
                    // Process each learning outcome
                    foreach (var loDTO in validLearningOutcomes)
                    {
                        if (!string.IsNullOrWhiteSpace(loDTO.Id) && existingLearningOutcomesDict.ContainsKey(loDTO.Id))
                        {
                            // Update existing learning outcome
                            var existingLO = existingLearningOutcomesDict[loDTO.Id];
                            existingLO.LOName = loDTO.LOName;
                            existingLO.Description = loDTO.Description;
                            existingLO.UpdatedDate = DateTime.Now;
                            _unitOfWork.learningOutcomeRepository.Update(existingLO);
                        }
                        else
                        {
                            // Add new learning outcome
                            var newLearningOutcome = new LearningOutcome()
                            {
                                Id = Guid.NewGuid().ToString(),
                                LOName = loDTO.LOName,
                                Description = loDTO.Description,
                                MaxScore = 100, // Default value
                                Weight = 1, // Default value
                                CourseId = courseDTO.Id,
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now
                            };
                            await _unitOfWork.learningOutcomeRepository.AddAsync(newLearningOutcome);
                        }
                    }
                    // Remove learning outcomes that are no longer in the list
                    var updatedLearningOutcomeIds = validLearningOutcomes.Where(lo => !string.IsNullOrWhiteSpace(lo.Id)).Select(lo => lo.Id).ToHashSet();
                    var learningOutcomesToRemove = existingLearningOutcomes.Where(lo => !updatedLearningOutcomeIds.Contains(lo.Id)).ToList();
                    if (learningOutcomesToRemove.Any())
                    {
                        _unitOfWork.learningOutcomeRepository.RemoveRange(learningOutcomesToRemove);
                    }
                }
                else
                {
                    // Remove all learning outcomes if no learning outcomes are provided
                    if (existingLearningOutcomes.Any())
                    {
                        _unitOfWork.learningOutcomeRepository.RemoveRange(existingLearningOutcomes);
                    }
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
