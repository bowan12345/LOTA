using LOTA.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LOTAWeb.Areas.Tutor.Controllers
{
    [Area(Roles.Role_Tutor)]
    [Authorize(Roles = Roles.Role_Tutor)]
    public class LOResultController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
