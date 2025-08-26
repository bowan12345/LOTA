using Microsoft.AspNetCore.Mvc;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LOResultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
