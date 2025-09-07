using LOTA.Model;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service;
using LOTA.Service.Service.IService;
using LOTA.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LOTAWeb.Areas.Tutor.Controllers
{
    [Area(Roles.Role_Tutor)]
    [Authorize(Roles = Roles.Role_Tutor)]
    public class LOResultController : Controller
    {
        private readonly ILOResultService _loResultService;
        private readonly ILOScoreService _loScoreService;
        private readonly ITrimesterCourseService _trimesterCourseService;
        private readonly ITrimesterService _trimesterService;
        private readonly ILogger<LOScoreController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public LOResultController(ILOResultService loResultService,ILOScoreService loScoreService, ITrimesterCourseService trimesterCourseService,
                                    ITrimesterService trimesterService, ILogger<LOScoreController> logger, UserManager<ApplicationUser> userManager)
        {
            _loResultService = loResultService;
            _loScoreService = loScoreService;
            _trimesterCourseService = trimesterCourseService;
            _trimesterService = trimesterService;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Get current user
                var currentUserId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(currentUserId))
                {
                    return RedirectToAction("Index", "Home", new { area = Roles.Role_Tutor });
                }

                // Get latest trimester
                var latestTrimester = await _trimesterService.GetLatestTrimesterAsync();
                if (latestTrimester == null || string.IsNullOrEmpty(latestTrimester.Id))
                {
                    return View(new List<CourseOfferingReturnDTO>());
                }

                // Filter by current tutor
                var tutorCourseOfferings = await _trimesterCourseService.GetTrimesterCoursesByTutorAndTrimesterAsync(currentUserId, latestTrimester.Id);

                return View(tutorCourseOfferings);
            }
            catch (Exception ex)
            {
                return View(new List<TrimesterCourseReturnDTO>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLOResultsByCourseOffering(string courseOfferingId)
        {

            var result = await _loResultService.GetLOResultsByCourseOfferingAsync(courseOfferingId);
            return Json(new { success = true, data = result });

        }

        [HttpPost]
        public async Task<IActionResult> UpdateRetakeScores([FromBody] RetakeRequestDTO retakeRequest)
        {
            try
            {
                await _loResultService.UpdateRetakeScoresAsync(retakeRequest);
                return Json(new { success = true, message = "Retake scores updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Retake scores updated failed" });
            }
        }



        [HttpGet]
        public async Task<IActionResult> GetFailedAssessmentsForRetake(string studentId, string courseOfferingId, string loName)
        {
            try
            {
                var failedAssessments = await _loResultService.GetFailedAssessmentsForRetakeAsync(
                    studentId,
                    courseOfferingId,
                    loName);
                return Json(new { success = true, data = failedAssessments });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
