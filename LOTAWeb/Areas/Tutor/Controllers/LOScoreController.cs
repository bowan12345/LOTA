using LOTA.Model;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service;
using LOTA.Service.Service.IService;
using LOTA.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LOTAWeb.Areas.Tutor.Controllers
{
    [Area(Roles.Role_Tutor)]
    [Authorize(Roles = Roles.Role_Tutor)]
    public class LOScoreController : Controller
    {
        private readonly ILOScoreService _loScoreService;
        private readonly ITrimesterCourseService _trimesterCourseService;
        private readonly ITrimesterService _trimesterService;
        private readonly ILogger<LOScoreController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public LOScoreController(ILOScoreService loScoreService, ITrimesterCourseService trimesterCourseService,
                                    ITrimesterService trimesterService, ILogger<LOScoreController> logger, UserManager<ApplicationUser> userManager)
        {
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
                return View(new List<CourseOfferingReturnDTO>());
            }
        }

        public async Task<IActionResult> Score(string id)
        {
            try
            {
                var courseOffering = await _loScoreService.GetCourseOfferingWithAssessmentsAsync(id);
                return View(courseOffering);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseOfferingDetailsByCourseOfferingId(string courseOfferingId)
        {
            try
            {
                if (string.IsNullOrEmpty(courseOfferingId))
                {
                    return Json(new { success = false, message = "Course offering ID is required" });
                }

                _logger.LogInformation($"Getting course offering details for ID: {courseOfferingId}");

                var courseOfferingDetails = await _loScoreService.GetCourseOfferingDetailsByCourseOfferingId(courseOfferingId);
                
                if (courseOfferingDetails == null)
                {
                    _logger.LogWarning($"Course offering not found for ID: {courseOfferingId}");
                    return Json(new { success = false, message = "Course offering not found" });
                }

                _logger.LogInformation($"Successfully retrieved course offering details for ID: {courseOfferingId}");
                return Json(new { success = true, data = courseOfferingDetails });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting course offering details for ID: {courseOfferingId}");
                return Json(new { success = false, message = "Error loading course offering details" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> BatchSaveStudentLOScores([FromBody] StudentLOScoresBatchSaveDTO batchSaveDTO)
        {
            try
            {
                _logger.LogInformation($"BatchSaveStudentLOScores called with StudentId: {batchSaveDTO.StudentId}, AssessmentId: {batchSaveDTO.AssessmentId}, LOScores count: {batchSaveDTO.LOScores?.Count ?? 0}");
                
                if (batchSaveDTO.LOScores == null || !batchSaveDTO.LOScores.Any())
                {
                    return Json(new { success = false, message = "No LO scores provided" });
                }
                
                await _loScoreService.BatchSaveStudentLOScoresAsync(
                    batchSaveDTO.StudentId, 
                    batchSaveDTO.AssessmentId, 
                    batchSaveDTO.LOScores);
                
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BatchSaveStudentLOScores");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> BatchSaveAllStudentsLOScores([FromBody] AllStudentsLOScoresBatchSaveDTO batchSaveDTO)
        {
            try
            {
                _logger.LogInformation($"BatchSaveAllStudentsLOScores called with AssessmentId: {batchSaveDTO.AssessmentId}, StudentScores count: {batchSaveDTO.StudentScores?.Count ?? 0}");
                
                if (batchSaveDTO.StudentScores == null || !batchSaveDTO.StudentScores.Any())
                {
                    return Json(new { success = false, message = "No student scores provided" });
                }
                
                await _loScoreService.BatchSaveAllStudentsLOScoresAsync(batchSaveDTO);
                
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BatchSaveAllStudentsLOScores");
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
