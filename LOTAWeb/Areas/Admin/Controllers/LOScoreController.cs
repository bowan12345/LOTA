using Microsoft.AspNetCore.Mvc;
using LOTA.Service.Service.IService;
using LOTA.Model.DTO.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class LOScoreController : Controller
    {
        private readonly ILOScoreService _loScoreService;
        private readonly ILogger<LOScoreController> _logger;

        public LOScoreController(ILOScoreService loScoreService, ILogger<LOScoreController> logger)
        {
            _loScoreService = loScoreService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var courseOfferings = await _loScoreService.GetCourseOfferingsWithAssessmentsAsync();
                return View(courseOfferings);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(new List<CourseOfferingAssessmentDTO>());
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
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }





        [HttpPost]
        public async Task<IActionResult> BatchSaveStudentLOScores([FromBody] StudentLOScoresBatchSaveDTO batchSaveDTO)
        {
            try
            {
                // Log the received data
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
