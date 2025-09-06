using LOTA.Service.Service.IService;
using LOTA.Utility;
using LOTAWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LOTAWeb.Controllers
{
    [Area(Roles.Role_Tutor)]
    [Authorize(Roles = Roles.Role_Tutor)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseService _courseService;

        public HomeController(ILogger<HomeController> logger, ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            return View();
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
