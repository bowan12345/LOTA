using LOTA.Model;
using LOTA.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LOTA.Model.DTO;
using LOTA.Model.DTO.Admin;
using ClosedXML.Excel;
using LOTA.Utility;
using Microsoft.AspNetCore.Authorization;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area(Roles.Role_Admin)]
    [Authorize(Roles = Roles.Role_Admin)]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ITrimesterService _trimesterService;
        private readonly ITrimesterCourseService _trimesterCourseService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseController(ICourseService courseService, ITrimesterService trimesterService, ITrimesterCourseService trimesterCourseService)
        {
            _courseService = courseService;
            _trimesterService = trimesterService;
            _trimesterCourseService = trimesterCourseService;
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
            // Controller layer data validation
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return Json(new { success = false, message = "Validation errors: " + string.Join(", ", errors) });
            }
            try
            {
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
            // Controller layer data validation
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return Json(new { success = false, message = "Validation errors: " + string.Join(", ", errors) });
            }
            try
            {
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
        /// <param name="qualificationId">Qualification ID to assign to all courses</param>
        /// <returns>JSON result with import status</returns>
        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file, string qualificationId)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return Json(new { success = false, message = "Please select a file to upload" });
                }

                // Validate qualification ID
                if (string.IsNullOrEmpty(qualificationId))
                {
                    return Json(new { success = false, message = "Please select a qualification before uploading" });
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

                using (var workbook = new XLWorkbook(stream)) 
                {
                    var worksheet = workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        return Json(new { success = false, message = "No worksheet found in the Excel file" });
                    }
                    // Get the used range
                    var usedRange = worksheet.RangeUsed();
                    // At least header + 1 data row
                    if (usedRange == null || usedRange.RowCount() < 2)
                    {
                        return Json(new { success = false, message = "Excel file is empty or has no data rows" });
                    }

                    // Validate headers
                    var headers = new List<string>();
                    var headerRow = worksheet.Row(1);
                    for (int col = 1; col <= usedRange.ColumnCount(); col++)
                    {
                        var headerValue = headerRow.Cell(col).Value.ToString()?.Trim();
                        if (!string.IsNullOrEmpty(headerValue))
                        {
                            headers.Add(headerValue);
                        }
                    }

                    // Check required headers
                    var requiredHeaders = new[] { "CourseName", "CourseCode"};
                    var missingHeaders = requiredHeaders.Where(h => !headers.Any(header =>
                        string.Equals(header, h, StringComparison.OrdinalIgnoreCase))).ToList();

                    if (missingHeaders.Any())
                    {
                        return Json(new { success = false, message = $"Missing required headers: {string.Join(", ", missingHeaders)}" });
                    }
                }


                var (successCount, errors) = await _courseService.ImportCoursesFromExcelAsync(stream, qualificationId);

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


        #region course offering
        public async Task<IActionResult> CourseOffering()
        {
            return View();
        }

        /// <summary>
        /// Get all course offerings with optional trimester filtering
        /// </summary>
        /// <param name="trimesterId">Optional trimester ID to filter by</param>
        /// <returns>JSON result with course offerings list</returns>
        [HttpGet]
        public async Task<IActionResult> GetCourseOfferings([FromQuery] string trimesterId = null)
        {
            try
            {
                var offerings = await _trimesterCourseService.GetAllTrimesterCoursesAsync();
                
                // Apply trimester filter if provided
                if (!string.IsNullOrEmpty(trimesterId))
                {
                    offerings = offerings.Where(o => o.TrimesterId == trimesterId).ToList();
                }
                
                return Json(new { success = true, data = offerings });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
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
                return Json(new { success = false, message = $"Failed to generate students template" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentsToCourseOffering([FromBody] AddStudentsToCourseOfferingDTO request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return Json(new { success = false, message = "Validation failed" + string.Join(", ", errors) });
            }
            try
            {
                await _courseService.AddStudentsToCourseOfferingAsync(request.CourseOfferingId, request.StudentIds, request.TrimesterId);
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
        public async Task<IActionResult> UploadStudentsExcelToCourseOffering([FromForm] IFormFile file, [FromForm] string courseOfferingId, [FromForm] string trimesterId)
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
                var (successCount, errors) = await _courseService.ImportStudentsFromExcelCourseOfferingAsync(courseOfferingId, trimesterId, stream);

                var message = $"Successfully imported {successCount} students.";
                if (errors.Count > 0)
                {
                    message += $" {errors.Count} errors occurred during import.";
                }

                return Json(new
                {
                    success = true,
                    message = message,
                    data = new
                    {
                        successCount,
                        errorCount = errors.Count,
                        // Return first 10 errors
                        errors = errors.Take(10).ToList() 
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
        public async Task<IActionResult> RemoveStudentFromCourseOffering([FromBody] RemoveStudentFromCourseDTO request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return Json(new { success = false, message = "Validation failed" + string.Join(", ", errors) });
            }
            try
            {
                await _courseService.RemoveStudentFromCourseOfferingAsync(request.CourseOfferingId, request.StudentId);
                return Json(new { success = true, message = "Student removed from course successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        /// <summary>
        /// Get course offering by ID
        /// </summary>
        /// <param name="id">Course offering ID</param>
        /// <returns>JSON result with course offering details</returns>
        [HttpGet]
        public async Task<IActionResult> GetCourseOffering(string id)
        {
            try
            {
                var offering = await _trimesterCourseService.GetTrimesterCourseByIdAsync(id);
                if (offering == null)
                {
                    return Json(new { success = false, message = "Course offering not found" });
                }

                return Json(new { success = true, data = offering });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Create new course offering
        /// </summary>
        /// <param name="offering">Course offering data</param>
        /// <returns>JSON result</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCourseOffering([FromBody] TrimesterCourseCreateDTO offering)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return Json(new { success = false, message = "Validation failed" + string.Join(", ", errors) });
            }
            try
            {
                var result = await _trimesterCourseService.CreateTrimesterCourseAsync(offering);
                return Json(new { success = true, data = result, message = "Course offering created successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while creating the course offering" });
            }
        }

        /// <summary>
        /// Update existing course offering
        /// </summary>
        /// <param name="offering">Course offering data</param>
        /// <returns>JSON result</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCourseOffering([FromBody] TrimesterCourseUpdateDTO offering)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return Json(new { success = false, message = "Validation failed" + string.Join(", ", errors) });
            }

            try
            {
                var result = await _trimesterCourseService.UpdateTrimesterCourseAsync(offering);
                return Json(new { success = true, data = result, message = "Course offering updated successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while updating the course offering" });
            }
        }

        /// <summary>
        /// Delete course offering
        /// </summary>
        /// <param name="id">Course offering ID</param>
        /// <returns>JSON result</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCourseOffering(string id)
        {
            
            if (string.IsNullOrWhiteSpace(id))
            {
                return Json(new { success = false, message = "Course offering ID is required" });
            }
            await _trimesterCourseService.DeleteTrimesterCourseAsync(id);
            return Json(new { success = true, message = "Course offering deleted successfully" });
               
        }


        /// <summary>
        /// Show Add Course Offering page
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public IActionResult AddCourseOffering()
        {
            return View();
        }

        /// <summary>
        /// Create multiple course offerings
        /// </summary>
        /// <param name="courseOfferings">List of course offerings</param>
        /// <returns>JSON result</returns>
        [HttpPost]
        public async Task<IActionResult> CreateMultipleCourseOfferings([FromBody] List<TrimesterCourseCreateDTO> courseOfferings)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return Json(new { success = false, message = "Validation failed" + string.Join(", ", errors) });
            }
            try
            {
                if (courseOfferings == null || !courseOfferings.Any())
                {
                    return Json(new { success = false, message = "No course offerings provided" });
                }

                var results = new List<object>();
                var successCount = 0;
                var errorCount = 0;

                foreach (var offering in courseOfferings)
                {
                    try
                    {
                        var result = await _trimesterCourseService.CreateTrimesterCourseAsync(offering);
                        results.Add(new { success = true, data = result });
                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        results.Add(new { success = false, error = ex.Message });
                        errorCount++;
                    }
                }

                var message = $"Successfully created {successCount} course offering(s)";
                if (errorCount > 0)
                {
                    message += $", {errorCount} failed";
                }

                return Json(new { 
                    success = successCount > 0, 
                    message = message,
                    results = results,
                    successCount = successCount,
                    errorCount = errorCount
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion course offering



    }
} 