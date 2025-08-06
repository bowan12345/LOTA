using LOTA.Model;
using LOTA.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TutorController : Controller
    {
        private readonly ITutorService _tutorService;

        public TutorController(ITutorService tutorService)
        {
            _tutorService = tutorService;
        }

        // GET: Admin/Tutor home page
        public async Task<IActionResult> Index()
        {
           
            //query all tutor information from tutorservice
            IEnumerable<ApplicationUser> tutorList = await _tutorService.GetAllTutorsAsync();
            //return all tutor information on the home page 
            return View(tutorList);
        }
    }
} 