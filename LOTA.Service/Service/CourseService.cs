using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Model.DTO;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service.IService;
using LOTA.Utility;
using ClosedXML.Excel;
using System.Data;

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

        public async Task<(int successCount, List<string> errors)> ImportCoursesFromExcelAsync(Stream fileStream)
        {
            var errors = new List<string>();
            var successCount = 0;

            try
            {
                using var workbook = new XLWorkbook(fileStream);
                var worksheet = workbook.Worksheet(1); // Get first worksheet
                var rows = worksheet.RowsUsed().Skip(1); // Skip header row

                foreach (var row in rows)
                {
                    try
                    {
                        var courseName = row.Cell("A").Value.ToString()?.Trim();
                        var courseCode = row.Cell("B").Value.ToString()?.Trim();
                        var description = row.Cell("C").Value.ToString()?.Trim();
                        var learningOutcomes = row.Cell("D").Value.ToString()?.Trim();

                        // Validate required fields
                        if (string.IsNullOrWhiteSpace(courseName))
                        {
                            errors.Add($"Row {row.RowNumber()}: Course Name is required");
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(courseCode))
                        {
                            errors.Add($"Row {row.RowNumber()}: Course Code is required");
                            continue;
                        }

                        // Check if course code already exists
                        var existingCourse = await _unitOfWork.courseRepository.GetAsync(c => c.CourseCode == courseCode);
                        if (existingCourse != null)
                        {
                            errors.Add($"Row {row.RowNumber()}: Course with code '{courseCode}' already exists");
                            continue;
                        }

                        // Create course
                        var course = new Course
                        {
                            Id = Guid.NewGuid().ToString(),
                            CourseName = courseName,
                            CourseCode = courseCode,
                            Description = description ?? "",
                            IsActive = true,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        };

                        await _unitOfWork.courseRepository.AddAsync(course);

                        // Process learning outcomes if provided
                        if (!string.IsNullOrWhiteSpace(learningOutcomes))
                        {
                            var loList = learningOutcomes.Split(';', StringSplitOptions.RemoveEmptyEntries)
                                .Select(lo => lo.Trim())
                                .Where(lo => !string.IsNullOrWhiteSpace(lo))
                                .ToList();

                            foreach (var lo in loList)
                            {
                                var learningOutcome = new LearningOutcome
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    LOName = lo,
                                    Description = lo, // Use the same value as description for now
                                    MaxScore = 100,
                                    Weight = 1,
                                    CourseId = course.Id,
                                    CreatedDate = DateTime.Now,
                                    UpdatedDate = DateTime.Now
                                };

                                await _unitOfWork.learningOutcomeRepository.AddAsync(learningOutcome);
                            }
                        }

                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Row {row.RowNumber()}: Error processing row - {ex.Message}");
                    }
                }

                if (successCount > 0)
                {
                    await _unitOfWork.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                errors.Add($"Error reading Excel file: {ex.Message}");
            }

            return (successCount, errors);
        }

        public async Task<byte[]> GenerateExcelTemplateAsync()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Courses");

            // Add headers
            worksheet.Cell("A1").Value = "CourseName";
            worksheet.Cell("B1").Value = "CourseCode";
            worksheet.Cell("C1").Value = "Description";
            worksheet.Cell("D1").Value = "LearningOutcomes";

            // Style headers
            var headerRange = worksheet.Range("A1:D1");
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            // Add sample data
            worksheet.Cell("A2").Value = "Introduction to Computer Science";
            worksheet.Cell("B2").Value = "CS101";
            worksheet.Cell("C2").Value = "Basic concepts of computer science";
            worksheet.Cell("D2").Value = "LO1; LO2; LO3";

            worksheet.Cell("A3").Value = "Advanced Programming";
            worksheet.Cell("B3").Value = "CS201";
            worksheet.Cell("C3").Value = "Advanced programming concepts";
            worksheet.Cell("D3").Value = "LO1; LO2";

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();

            // Add data validation for required fields
            var courseNameValidation = worksheet.Range("A2:A1000").CreateDataValidation();
            courseNameValidation.Custom("=LEN(A2)>0");
            courseNameValidation.ErrorMessage = "Course Name is required";

            var courseCodeValidation = worksheet.Range("B2:B1000").CreateDataValidation();
            courseCodeValidation.Custom("=LEN(B2)>0");
            courseCodeValidation.ErrorMessage = "Course Code is required";

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
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
