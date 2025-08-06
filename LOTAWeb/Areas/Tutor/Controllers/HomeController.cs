using LOTA.Model;
using LOTA.Service.Service.IService;
using LOTAWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LOTAWeb.Controllers
{
    [Area("Tutor")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseService _courseService;

        public HomeController(ILogger<HomeController> logger, ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        /*public IActionResult Index()
        {
            return View();
        }*/
        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllCoursesAsync(null);
            return View(courses);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
