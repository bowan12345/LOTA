using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using LOTA.Utility;
using LOTA.Model;
using LOTA.Service.Service.IService;

namespace LOTAWeb.Areas.Student.Controllers
{
    [Area(Roles.Role_Student)]
    [Authorize(Roles = Roles.Role_Student)]
    public class LOResultController : Controller
    {
        private readonly ILOResultService _loResultService;
        private readonly UserManager<ApplicationUser> _userManager;

        public LOResultController(ILOResultService loResultService, UserManager<ApplicationUser> userManager)
        {
            _loResultService = loResultService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var currentUserId = _userManager.GetUserId(User);
                var studentResults = await _loResultService.GetStudentLOResultsAsync(currentUserId);
                return View(studentResults);
            }
            catch (Exception ex)
            {
                return View(new LOTA.Model.DTO.Student.StudentLOResultDTO());
            }
        }
    }
}
