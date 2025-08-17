using LOTA.Model;
using LOTA.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using LOTAWeb.Models;
using LOTA.Service.Service;
using LOTA.Model.DTO;
using LOTA.Model.DTO.Admin;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ITrimesterService _trimesterService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseController(ICourseService courseService, ITrimesterService trimesterService)
        {
            _courseService = courseService;
            _trimesterService = trimesterService;
        }

        // GET: Admin/course home page
        public async Task<IActionResult> Index([FromQuery] string searchTerm = "")
        {
            IEnumerable<CourseReturnDTO> courseList;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                //query all courseList information by filter
                 courseList = await _courseService.GetCoursesByNameOrCodeAsync(searchTerm);
            }
            else
            {
                //query all courseList information from courseservice
                 courseList = await _courseService.GetAllCoursesAsync();
            }

            // Pass search term to view for maintaining search state
            ViewBag.SearchTerm = searchTerm;

            //return all course information on the home page 
            return View(courseList);
        }
        public async Task<IActionResult> Details(string id) 
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction("Index");
            }
            
            // Query course information by course id
            var courseReturnDTO = await _courseService.GetCourseByIdAsync(id);
            
            if (courseReturnDTO == null)
            {
                return RedirectToAction("Index");
            }
            
            return View(courseReturnDTO);
        }

        /// <summary>
        ///  search courses by course name or course code
        /// </summary>
        /// <param name="courseSearchItem"> course name or course code </param>
        /// <returns> course list </returns>
        // GET: Admin/Course/SearchCourses
        [HttpGet]
        public async Task<IActionResult> SearchCourses([FromQuery] string courseSearchItem = "")
        {
            try
            {
                var courseList = await _courseService.GetCoursesByNameOrCodeAsync(courseSearchItem);
                return Json(new { success = true, data = courseList });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchCourses: {ex.Message}");
                return Json(new { success = false, message = $"Failed to search courses: {ex.Message}" });
            }
        }



        /// <summary>
        /// Create a new course
        /// </summary>
        /// <param name="courseCreateDTO">Course data from frontend</param>
        /// <returns>JSON result</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseCreateDTO courseCreateDTO) 
        {
            try
            {
                // Controller layer data validation
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return Json(new { success = false, message = "Validation errors: " + string.Join(", ", errors) });
                }

                // Check if course code already exists
                var existingCourse = await _courseService.GetCourseByCodeAsync(courseCreateDTO.CourseCode);
                if (existingCourse != null)
                {
                    return Json(new { success = false, message = "Course with this code already exists" });
                }

                // Call Service layer to handle business logic
                var result = await _courseService.CreateCourseAsync(courseCreateDTO);
                
                return Json(new { success = true, message = "Course created successfully", data = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateCourse: {ex.Message}");
                return Json(new { success = false, message = $"Failed to create course: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get course by ID for editing
        /// </summary>
        /// <param name="id">Course ID</param>
        /// <returns>JSON result with course data</returns>
        [HttpGet]
        public async Task<IActionResult> GetCourseById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return Json(new { success = false, message = "Course ID is required" });
                }

                var course = await _courseService.GetCourseByIdAsync(id);
                if (course == null)
                {
                    return Json(new { success = false, message = "Course not found" });
                }

                return Json(new { success = true, data = course });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetCourseById: {ex.Message}");
                return Json(new { success = false, message = $"Failed to get course: {ex.Message}" });
            }
        }

        /// <summary>
        /// Update an existing course
        /// </summary>
        /// <param name="courseUpdateDTO">Updated course data</param>
        /// <returns>JSON result</returns>
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] CourseUpdateDTO courseUpdateDTO)
        {
            try
            {
                // Controller layer data validation
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return Json(new { success = false, message = "Validation errors: " + string.Join(", ", errors) });
                }

                // Call Service layer to handle business logic
                await _courseService.UpdateCourse(courseUpdateDTO);
                
                return Json(new { success = true, message = "Course updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateCourse: {ex.Message}");
                return Json(new { success = false, message = $"Failed to update course: {ex.Message}" });
            }
        }

        /// <summary>
        /// Delete a course
        /// </summary>
        /// <param name="id">Course ID</param>
        /// <returns>JSON result</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return Json(new { success = false, message = "Course ID is required" });
                }

                // Call Service layer to handle business logic
                await _courseService.RemoveCourse(id);
                
                return Json(new { success = true, message = "Course deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteCourse: {ex.Message}");
                return Json(new { success = false, message = $"Failed to delete course: {ex.Message}" });
            }
        }

        /// <summary>
        /// Delete multiple courses
        /// </summary>
        /// <param name="request">Request containing list of course IDs</param>
        /// <returns>JSON result</returns>
        [HttpPost]
        public async Task<IActionResult> DeleteSelected([FromBody] DeleteSelectedDTO request)
        {
            try
            {
                if (request?.Ids == null || !request.Ids.Any())
                {
                    return Json(new { success = false, message = "No courses selected for deletion" });
                }

                var deletedCount = 0;
                var errors = new List<string>();

                foreach (var id in request.Ids)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(id))
                        {
                            errors.Add("Course ID is required");
                            continue;
                        }

                        // Call Service layer to handle business logic
                        await _courseService.RemoveCourse(id);
                        deletedCount++;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Failed to delete course {id}: {ex.Message}");
                    }
                }

                if (deletedCount > 0)
                {
                    var message = $"Successfully deleted {deletedCount} course(s)";
                    if (errors.Any())
                    {
                        message += $". {errors.Count} error(s) occurred: {string.Join("; ", errors)}";
                    }
                    return Json(new { success = true, message = message, deletedCount, errorCount = errors.Count });
                }
                else
                {
                    return Json(new { success = false, message = "No courses were deleted. Errors: " + string.Join("; ", errors) });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in batch deletion: {ex.Message}");
                return Json(new { success = false, message = "An error occurred during batch deletion. Please try again or contact support." });
            }
        }

        /// <summary>
        /// Upload Excel file to import courses
        /// </summary>
        /// <param name="file">Excel file</param>
        /// <returns>JSON result with import status</returns>
        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return Json(new { success = false, message = "Please select a file to upload" });
                }

                // Validate file extension
                var allowedExtensions = new[] { ".xlsx", ".xls" };
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return Json(new { success = false, message = "Please select a valid Excel file (.xlsx or .xls)" });
                }

                // Validate file size (max 10MB)
                if (file.Length > 10 * 1024 * 1024)
                {
                    return Json(new { success = false, message = "File size must be less than 10MB" });
                }

                using var stream = file.OpenReadStream();
                var (successCount, errors) = await _courseService.ImportCoursesFromExcelAsync(stream);

                var message = $"Successfully imported {successCount} courses.";
                if (errors.Count > 0)
                {
                    message += $" {errors.Count} errors occurred during import.";
                }

                return Json(new { 
                    success = true, 
                    message = message,
                    data = new { 
                        successCount, 
                        errorCount = errors.Count,
                        errors = errors.Take(10).ToList() // Return first 10 errors
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UploadExcel: {ex.Message}");
                return Json(new { success = false, message = $"Failed to upload Excel file: {ex.Message}" });
            }
        }

        /// <summary>
        /// Download Excel template for course import
        /// </summary>
        /// <returns>Excel file download</returns>
        [HttpGet]
        public async Task<IActionResult> DownloadTemplate()
        {
            try
            {
                var excelBytes = await _courseService.GenerateExcelTemplateAsync();
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CourseImportTemplate.xlsx");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DownloadTemplate: {ex.Message}");
                return Json(new { success = false, message = $"Failed to generate template: {ex.Message}" });
            }
        }

        /// <summary>
        /// Download Excel template for uploading students to course
        /// </summary>
        /// <returns>Excel file download</returns>
        [HttpGet]
        public async Task<IActionResult> DownloadStudentsTemplate()
        {
            try
            {
                var excelBytes = await _courseService.GenerateStudentsExcelTemplateAsync();
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StudentsUploadTemplate.xlsx");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DownloadStudentsTemplate: {ex.Message}");
                return Json(new { success = false, message = $"Failed to generate students template: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentsToCourse([FromBody] AddStudentsToCourseDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return Json(new { success = false, message = "Validation failed", errors });
                }

                await _courseService.AddStudentsToCourseAsync(request.CourseId, request.StudentIds, request.TrimesterId);
                return Json(new { success = true, message = "Students added to course successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// enroll students
        /// </summary>
        /// <param name="file"> excel file</param>
        /// <param name="courseId"> current course id</param>
        /// <param name="academicYear"> which academic year</param>
        /// <param name="trimesterNumber"> which trimester number</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadStudentsExcel([FromForm] IFormFile file, [FromForm] string courseId, [FromForm] string trimesterId)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return Json(new { success = false, message = "Please select a file to upload" });
                }

                // Validate file extension
                var allowedExtensions = new[] { ".xlsx", ".xls"};
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return Json(new { success = false, message = "Please select a valid Excel file (.xlsx or .xls)" });
                }

                // Validate file size (max 10MB)
                if (file.Length > 10 * 1024 * 1024)
                {
                    return Json(new { success = false, message = "File size must be less than 10MB" });
                }

                using var stream = file.OpenReadStream();
                var (successCount, errors) = await _courseService.ImportStudentsFromExcelAsync(courseId, trimesterId, stream);

                var message = $"Successfully imported {successCount} students.";
                if (errors.Count > 0)
                {
                    message += $" {errors.Count} errors occurred during import.";
                }

                return Json(new { 
                    success = true, 
                    message = message,
                    data = new { 
                        successCount, 
                        errorCount = errors.Count,
                        errors = errors.Take(10).ToList() // Return first 10 errors
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UploadStudentsExcel: {ex.Message}");
                return Json(new { success = false, message = $"Failed to upload Excel file: {ex.Message}" });
            }
        }



        [HttpPost]
        public async Task<IActionResult> RemoveStudentFromCourse([FromBody] RemoveStudentFromCourseDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return Json(new { success = false, message = "Validation failed", errors });
                }

                await _courseService.RemoveStudentFromCourseAsync(request.CourseId, request.StudentId);
                return Json(new { success = true, message = "Student removed from course successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get courses tree structure with qualifications and trimesters
        /// </summary>
        /// <returns>JSON result with tree structure</returns>
        [HttpGet]
        public async Task<IActionResult> GetCoursesTree()
        {
            try
            {
                var courses = await _courseService.GetAllCoursesAsync();
                var trimesters = await _trimesterService.GetActiveTrimestersAsync();
                
                // Group courses by qualification
                var treeData = courses
                    .Where(c => !string.IsNullOrEmpty(c.QualificationId))
                    .GroupBy(c => new { 
                        QualificationId = c.QualificationId, 
                        QualificationName = c.QualificationName,
                        QualificationType = c.QualificationType,
                        Level = c.Level
                    })
                    .Select(qualGroup => new
                    {
                        QualificationId = qualGroup.Key.QualificationId,
                        QualificationName = qualGroup.Key.QualificationName,
                        QualificationType = qualGroup.Key.QualificationType,
                        Level = qualGroup.Key.Level,
                        Courses = qualGroup.Select(course => new
                        {
                            CourseId = course.Id,
                            CourseName = course.CourseName,
                            CourseCode = course.CourseCode,
                            Description = course.Description,
                            Trimesters = trimesters.Select(t => new
                            {
                                TrimesterId = t.Id,
                                AcademicYear = t.AcademicYear,
                                TrimesterNumber = t.TrimesterNumber,
                                DisplayName = $"Trimester {t.TrimesterNumber} ({t.AcademicYear})"
                            }).ToList()
                        }).ToList()
                    })
                    .ToList();

                return Json(new { success = true, data = treeData });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
} 