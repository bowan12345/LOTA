using Microsoft.AspNetCore.Mvc;
using LOTA.Service.Service.IService;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ITutorService _tutorService;
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ITutorService tutorService,
            ICourseService courseService,
            IStudentService studentService,
            ILogger<HomeController> logger)
        {
            _tutorService = tutorService;
            _courseService = courseService;
            _studentService = studentService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardStats()
        {
            try
            {
                _logger.LogInformation("GetDashboardStats called");
                
                var tutors = await _tutorService.GetAllTutorsAsync();
                var courses = await _courseService.GetAllCoursesAsync();
                var students = await _studentService.GetAllStudentsAsync();
                var stats = new
                {
                    TotalTutors = tutors?.Count() ?? 0,
                    TotalCourses = courses?.Count() ?? 0,
                    TotalStudents = students?.Count() ?? 0
                };

                return Json(new { success = true, data = stats });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetDashboardStats");
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
} 