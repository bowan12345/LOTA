using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LOTA.Model;
using LOTA.Service.Service.IService;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using LOTA.Model.DTO.Admin;
using LOTA.Utility;
using Microsoft.IdentityModel.Tokens;
using LOTA.Service.Service;

namespace LOTAWeb.Areas.Tutor.Controllers
{
    [Area(Roles.Role_Tutor)]
    [Authorize(Roles = Roles.Role_Tutor)]
    public class CourseController : Controller
    {
        private readonly ITrimesterCourseService _trimesterCourseService;
        private readonly ICourseService _courseService;
        private readonly ITrimesterService _trimesterService;
        private readonly IStudentService _studentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseController(ITrimesterCourseService trimesterCourseService, ICourseService courseService, ITrimesterService trimesterService, UserManager<ApplicationUser> userManager, IStudentService studentService)
        {
            _trimesterCourseService = trimesterCourseService;
            _courseService = courseService;
            _trimesterService = trimesterService;
            _studentService = studentService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Get current user (tutor)
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(currentUserId))
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            // Get latest trimester
            var latestTrimester = await _trimesterService.GetLatestTrimesterAsync();
            if (latestTrimester == null || string.IsNullOrEmpty(latestTrimester.Id))
            {
                return View(new List<CourseReturnDTO>());
            }

            // Get course offerings for current tutor in latest trimester
            var tutorTrimesterCourses = await _trimesterCourseService.GetTrimesterCoursesByTutorAndTrimesterAsync(currentUserId,latestTrimester.Id);
            
            // Extract unique course IDs from trimester courses
            var courseIds = tutorTrimesterCourses.Select(tc => tc.CourseId).Distinct().ToList();
            
            // Get course details for each course ID
            var courses = await _courseService.GetCourseByIdsAsync(courseIds);
           
            return View(courses);
        }

        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> GetCourseById(string id)
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

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return PartialView("_CourseEdit", course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromBody] CourseUpdateDTO courseUpdateDTO)
        {
            try
            {
                //tutor doesnt uodate these fields
                courseUpdateDTO.CourseCode = string.Empty;
                courseUpdateDTO.CourseName = string.Empty;
                courseUpdateDTO.QualificationId = string.Empty;
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




        public async Task<IActionResult> CourseOffering()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetCourseOffering(string courseId)
        {
            try
            {
                // Get current user
                var currentUserId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(currentUserId))
                {
                    return Json(new { success = false, message = "User not authenticated" });
                }

                if (string.IsNullOrEmpty(courseId))
                {
                    return Json(new { success = false, message = "Course ID is required" });
                }

                // Get course information
                var course = await _courseService.GetCourseByIdAsync(courseId);
                if (course == null)
                {
                    return Json(new { success = false, message = "Course not found" });
                }

                // Get latest trimester
                var latestTrimester = await _trimesterService.GetLatestTrimesterAsync();
                if (latestTrimester == null)
                {
                    return Json(new { success = false, message = "No trimester available" });
                }

                // Get course offering for this course and latest trimester
                var courseOfferings = await _trimesterCourseService.GetTrimesterCoursesByCourseAsync(courseId);
                var courseOffering = courseOfferings?.FirstOrDefault(co =>
                    co.TrimesterId == latestTrimester.Id && co.TutorId == currentUserId);

                return Json(new { success = true, data = courseOffering });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Loading course failed" });
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetEnrolledStudents(string courseOfferingId, string trimesterId)
        {
            try
            {
                var students = await _studentService.GetEnrolledStudentsAsync(courseOfferingId, trimesterId);
                return Json(new { success = true, data = students });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
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



    }
}
