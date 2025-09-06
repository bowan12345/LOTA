using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LOTA.Model;
using LOTA.Service.Service.IService;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using LOTA.Model.DTO.Admin;
using LOTA.Utility;
using Microsoft.IdentityModel.Tokens;

namespace LOTAWeb.Areas.Tutor.Controllers
{
    [Area(Roles.Role_Tutor)]
    [Authorize(Roles = Roles.Role_Tutor)]
    public class CourseController : Controller
    {
        private readonly ITrimesterCourseService _trimesterCourseService;
        private readonly ICourseService _courseService;
        private readonly ITrimesterService _trimesterService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseController(ITrimesterCourseService trimesterCourseService, ICourseService courseService, ITrimesterService trimesterService, UserManager<ApplicationUser> userManager)
        {
            _trimesterCourseService = trimesterCourseService;
            _courseService = courseService;
            _trimesterService = trimesterService;
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
    }
}
