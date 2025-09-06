using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Model.DTO;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service.IService;
using LOTA.Utility;
using ClosedXML.Excel;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using Azure.Core;

namespace LOTA.Service.Service
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CourseReturnDTO> CreateCourseAsync(CourseCreateDTO courseDTO)
        {
            //throw new NotImplementedException("TDD Red phase: method not implemented yet");
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
                QualificationId = courseDTO.QualificationId,
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
                        CourseId = courseId, // Set foreign key relationship
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }).ToList();
                
                    // Save learning outcomes to database
                    await _unitOfWork.learningOutcomeRepository.AddRangeAsync(learningOutcomes);
                }
            }
            // Save changes to database
            await _unitOfWork.SaveAsync();
            // Return creation result
            return new CourseReturnDTO()
            {
                Id = courseId,
                CourseCode = courseDTO.CourseCode,
                CourseName = courseDTO.CourseName,
                Description = courseDTO.Description,
                QualificationId = courseDTO.QualificationId
            };
        }

        public async Task<IEnumerable<CourseReturnDTO>> GetAllCoursesAsync()
        {
            //throw new NotImplementedException("TDD Red phase: method not implemented yet");
            // get all courses based on filter conditions
            IEnumerable<Course> courses = await _unitOfWork.courseRepository.GetAllAsync(includeProperties: "LearningOutcomes,Qualification,Qualification.QualificationType");
            return courses.Select(MapToDTO);

        }

        public async Task<CourseReturnDTO> GetCourseByCodeAsync(string courseCode)
        {
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
            
            Course course = await _unitOfWork.courseRepository.GetByIdAsync(courseId, includeProperties: "LearningOutcomes,Qualification,Qualification.QualificationType");
            if (course == null)
            {
                return null;
            }
            return MapToDTO(course);
        }

        public async Task<IEnumerable<CourseReturnDTO>> GetCoursesByNameOrCodeAsync(string courseSearchItem)
        {

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
            IEnumerable<Course> courses = await _unitOfWork.courseRepository.GetAllAsync(filter,includeProperties: "LearningOutcomes,Qualification,Qualification.QualificationType");
            return courses.Select(MapToDTO);
        }

        public async Task RemoveCourse(string courseId)
        {
          
            if (string.IsNullOrEmpty(courseId))
            {
                throw new NullReferenceException("courseId is empty");
            }
            //remove course
            try
            {
                _unitOfWork.courseRepository.Remove(courseId);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
           

        public async Task UpdateCourse(CourseUpdateDTO courseDTO)
        {
          
            try
            {
                if (courseDTO == null)
                {
                    throw new NullReferenceException("Course is empty");
                }
                var course = await _unitOfWork.courseRepository.GetByIdAsync(courseDTO.Id);
                if (course == null)
                {
                    throw new NullReferenceException("Course not found");
                }
                var courseCode = await _unitOfWork.courseRepository.GetCourseByCodeAsync(courseDTO.CourseCode);
                if (courseCode != null && courseCode.Id != course.Id) 
                {
                    throw new NullReferenceException("Course code has already existed");
                }
                if (!string.IsNullOrEmpty(courseDTO.CourseCode))
                {
                    course.CourseCode = courseDTO.CourseCode;
                }
                if (!string.IsNullOrEmpty(courseDTO.CourseName))
                {
                    course.CourseName = courseDTO.CourseName;
                }
                if (!string.IsNullOrEmpty(courseDTO.QualificationId))
                {
                    course.QualificationId = courseDTO.QualificationId;
                }
                if (!string.IsNullOrEmpty(courseDTO.Description))
                {
                    course.Description = courseDTO.Description;
                }
                course.UpdatedDate = DateTime.Now;

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
                        _unitOfWork.learningOutcomeRepository.RemoveRange(learningOutcomesToRemove.Select(lo => lo.Id));
                    }
                }
                else
                {
                    // Remove all learning outcomes if no learning outcomes are provided
                    if (existingLearningOutcomes.Any())
                    {
                        _unitOfWork.learningOutcomeRepository.RemoveRange(existingLearningOutcomes.Select(lo=>lo.Id));
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
                        if (existingCourse != null && existingCourse.Count()>0)
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
                            Description = "",
                            QualificationId = null,
                            IsActive = true,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        };

                        await _unitOfWork.courseRepository.AddAsync(course);

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

            // Style headers
            var headerRange = worksheet.Range("A1:B1");
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            // Add sample data
            worksheet.Cell("A2").Value = "Introduction to Computer Science";
            worksheet.Cell("B2").Value = "CS101";

            worksheet.Cell("A3").Value = "Advanced Programming";
            worksheet.Cell("B3").Value = "CS201";

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
                Level = course.Qualification?.Level ?? 0, // Get Level from Qualification
                IsActive = course.IsActive,
                CreatedDate = course.CreatedDate,
                UpdatedDate = course.UpdatedDate,
                QualificationId = course.QualificationId,
                QualificationName = course.Qualification?.QualificationName,
                QualificationType = course.Qualification?.QualificationType?.QualificationTypeName,
                LearningOutcomes = course.LearningOutcomes?.Select(lo => new LearningOutcomeDTO
                {
                    Id = lo.Id,
                    LOName = lo.LOName,
                    Description = lo.Description,
                    CreatedDate = lo.CreatedDate,
                    UpdatedDate = lo.UpdatedDate
                }).ToList() ?? new List<LearningOutcomeDTO>()
            };
        }


        public async Task AddStudentsToCourseOfferingAsync(string courseOfferingId, List<string> studentIds, string trimesterId)
        {
            
            // Validate trimester exists
            var trimester = await _unitOfWork.trimesterRepository.GetAsync(t => t.Id == trimesterId);
            if (!trimester.Any())
            {
                throw new InvalidOperationException($"Trimester not found");
            }

            foreach (var studentId in studentIds)
            {
                // Check if student is already enrolled in this course for this trimester
                var existingEnrollment = await _unitOfWork.studentCourseRepository.GetAsync(sc => 
                    sc.StudentId == studentId && 
                    sc.CourseOfferingId == courseOfferingId && 
                    sc.TrimesterId == trimesterId);

                if (!existingEnrollment.Any())
                {
                    var studentCourse = new StudentCourse
                    {
                        Id = Guid.NewGuid().ToString(),
                        StudentId = studentId,
                        CourseOfferingId = courseOfferingId,
                        TrimesterId = trimesterId,
                        IsActive = true,
                        RegistrationDate = DateTime.UtcNow,
                        CreatedDate = DateTime.UtcNow
                    };

                    await _unitOfWork.studentCourseRepository.AddAsync(studentCourse);
                }
                else
                {
                    // Student is already enrolled in this course for this trimester
                    throw new InvalidOperationException($"Student is already enrolled in course for trimester");
                }
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveStudentFromCourseOfferingAsync(string courseOfferingId, string studentId)
        {
            //throw new NotImplementedException("TDD Red phase: method not implemented yet");
            var enrollment = await _unitOfWork.studentCourseRepository.GetByStudentAndCourseAsync(studentId, courseOfferingId);
            if (enrollment != null)
            {
                _unitOfWork.studentCourseRepository.Remove(enrollment.Id);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<(int successCount, List<string> errors)> ImportStudentsFromExcelCourseOfferingAsync(string courseOfferingId, string trimesterId, Stream fileStream)
        {
            var errors = new List<string>();
            var successCount = 0;

            try
            {
                // Validate trimester exists
                var trimester = await _unitOfWork.trimesterRepository.GetAsync(t => t.Id == trimesterId);
                if (!trimester.Any())
                {
                    throw new InvalidOperationException($"Trimester not found");
                }

                using var workbook = new XLWorkbook(fileStream);
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RowsUsed().Skip(1); // Skip header row

                // De-duplicate within the uploaded file by (studentId, email)
                var seenStudentKeySet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var row in rows)
                {
                    try
                    {
                        var studentId = row.Cell("A").Value.ToString()?.Trim();
                        var email = row.Cell("B").Value.ToString()?.Trim();

                        if (string.IsNullOrEmpty(studentId) || string.IsNullOrEmpty(email))
                        {
                            errors.Add($"Row {row.RowNumber()}: Student ID or Email is empty");
                            continue;
                        }

                        var compositeKey = $"{studentId}|{email}";
                        if (seenStudentKeySet.Contains(compositeKey))
                        {
                            errors.Add($"Row {row.RowNumber()}: Duplicate entry for StudentId '{studentId}' and Email '{email}' in file");
                            continue;
                        }
                        seenStudentKeySet.Add(compositeKey);

                        // Find student by Student ID or Email
                        var student = await _unitOfWork.studentRepository.GetAsync(s => 
                            s.StudentNo == studentId && s.Email == email);

                        if (!student.Any())
                        {
                            errors.Add($"Row {row.RowNumber()}: Student not found with ID '{studentId}' or Email '{email}'");
                            continue;
                        }

                        var studentEntity = student.First();

                        // Check if student is already enrolled in this course for this trimester
                        var existingEnrollment = await _unitOfWork.studentCourseRepository.GetAsync(sc => 
                            sc.StudentId == studentEntity.Id && 
                            sc.CourseOfferingId == courseOfferingId && 
                            sc.TrimesterId == trimesterId);

                        if (!existingEnrollment.Any())
                        {
                            var studentCourse = new StudentCourse
                            {
                                Id = Guid.NewGuid().ToString(),
                                StudentId = studentEntity.Id,
                                CourseOfferingId = courseOfferingId,
                                TrimesterId = trimesterId,
                                IsActive = true,
                                RegistrationDate = DateTime.UtcNow,
                                CreatedDate = DateTime.UtcNow
                            };

                            await _unitOfWork.studentCourseRepository.AddAsync(studentCourse);
                            successCount++;
                        }
                        else
                        {
                            // Student is already enrolled in this course for this trimester
                            errors.Add($"Row {row.RowNumber()}: Student '{studentId}' is already enrolled in this course for trimester {trimesterId}");
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Row {row.RowNumber()}: Error processing row - {ex.Message}");
                    }
                }

                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                errors.Add($"General error: {ex.Message}");
            }

            return (successCount, errors);
        }

        public async Task RemoveRangeCourse(List<string> courseIds)
        {
            if (courseIds == null || !courseIds.Any())
            {
                throw new NullReferenceException ("No courses selected for deletion" );
            }
            //remove courses
            try
            {   
                _unitOfWork.courseRepository.RemoveRange(courseIds);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        /// <summary>
        /// Generate Excel template for uploading students to course
        /// </summary>
        /// <returns>Excel file as byte array</returns>
        public async Task<byte[]> GenerateStudentsExcelTemplateAsync()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Students");

            // Add headers
            worksheet.Cell("A1").Value = "StudentId";
            worksheet.Cell("B1").Value = "Email";

            // Style headers
            var headerRange = worksheet.Range("A1:B1");
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
            headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            // Add sample data
            worksheet.Cell("A2").Value = "STU001";
            worksheet.Cell("B2").Value = "student1@example.com";

            worksheet.Cell("A3").Value = "STU002";
            worksheet.Cell("B3").Value = "student2@example.com";

            worksheet.Cell("A4").Value = "STU003";
            worksheet.Cell("B4").Value = "student3@example.com";

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();

            // Add data validation for required fields
            var studentIdValidation = worksheet.Range("A2:A1000").CreateDataValidation();
            studentIdValidation.Custom("=LEN(A2)>0");
            studentIdValidation.ErrorMessage = "Student ID is required";

            var emailValidation = worksheet.Range("B2:B1000").CreateDataValidation();
            emailValidation.Custom("=LEN(B2)>0");
            emailValidation.ErrorMessage = "Email is required";

            // Add instructions sheet
            var instructionsSheet = workbook.Worksheets.Add("Instructions");
            instructionsSheet.Cell("A1").Value = "Instructions for Uploading Students Excel";
            instructionsSheet.Cell("A1").Style.Font.Bold = true;
            instructionsSheet.Cell("A1").Style.Font.FontSize = 14;

            instructionsSheet.Cell("A3").Value = "Required Format:";
            instructionsSheet.Cell("A3").Style.Font.Bold = true;
            instructionsSheet.Cell("A4").Value = "• Column A: Student ID (Student Number)";
            instructionsSheet.Cell("A5").Value = "• Column B: Email Address";
            instructionsSheet.Cell("A6").Value = "";
            instructionsSheet.Cell("A7").Value = "Important Notes:";
            instructionsSheet.Cell("A7").Style.Font.Bold = true;
            instructionsSheet.Cell("A8").Value = "• Both Student ID and Email must match existing student records";
            instructionsSheet.Cell("A9").Value = "• Students will be enrolled in the selected course and trimester";
            instructionsSheet.Cell("A10").Value = "• Duplicate enrollments will be skipped";
            instructionsSheet.Cell("A11").Value = "• Only active students can be enrolled";

            // Auto-fit instructions sheet
            instructionsSheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public async Task<IEnumerable<CourseReturnDTO>> GetCourseByIdsAsync(List<string> courseIds)
        {
            if (courseIds == null || !courseIds.Any())
            {
                return new List<CourseReturnDTO>();
            }

            var courses = await _unitOfWork.courseRepository.GetAllAsync(
                filter: c => courseIds.Contains(c.Id),
                includeProperties: "LearningOutcomes,Qualification,Qualification.QualificationType"
            );

            return courses.Select(MapToDTO);
        }

        public async Task UpdateCourseDescriptionAndLOsAsync(string courseId, string description, string learningOutcomeNames, string learningOutcomeDescriptions)
        {
            if (string.IsNullOrEmpty(courseId))
            {
                throw new ArgumentException("Course ID cannot be null or empty", nameof(courseId));
            }

            // Get the course with learning outcomes
            var course = await _unitOfWork.courseRepository.GetByIdAsync(courseId, includeProperties: "LearningOutcomes");
            if (course == null)
            {
                throw new InvalidOperationException($"Course with ID {courseId} not found");
            }

            // Update description
            course.Description = description;
            course.UpdatedDate = DateTime.Now;

            // Parse learning outcomes
            var loNames = learningOutcomeNames?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
            var loDescriptions = learningOutcomeDescriptions?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? new string[0];

            // Remove existing learning outcomes
            if (course.LearningOutcomes.Any())
            {
                _unitOfWork.learningOutcomeRepository.RemoveRange(course.LearningOutcomes.Select(lo => lo.Id));
            }

            // Add new learning outcomes
            for (int i = 0; i < Math.Max(loNames.Length, loDescriptions.Length); i++)
            {
                var loName = i < loNames.Length ? loNames[i].Trim() : "";
                var loDescription = i < loDescriptions.Length ? loDescriptions[i].Trim() : "";

                if (!string.IsNullOrEmpty(loName))
                {
                    var learningOutcome = new LearningOutcome
                    {
                        Id = Guid.NewGuid().ToString(),
                        LOName = loName,
                        Description = loDescription,
                        CourseId = courseId,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };

                    await _unitOfWork.learningOutcomeRepository.AddAsync(learningOutcome);
                }
            }

            // Update course
            _unitOfWork.courseRepository.Update(course);

            // Save changes
            await _unitOfWork.SaveAsync();
        }
    }
}
