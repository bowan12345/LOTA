using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LOTA.Utility;

namespace LOTAWeb.Areas.Student.Controllers
{
    [Area(Roles.Role_Student)]
    [Authorize(Roles = Roles.Role_Student)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
