using Microsoft.AspNetCore.Mvc;
using LOTA.Service.Service.IService;
using LOTA.Model.DTO.Admin;
using System.Text.Json;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LOResultController : Controller
    {
        private readonly ILOResultService _loResultService;

        public LOResultController(ILOResultService loResultService)
        {
            _loResultService = loResultService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Get latest trimester course offerings for dropdown
                var courseOfferings = await _loResultService.GetLatestTrimesterCourseOfferingsAsync();
                ViewBag.CourseOfferings = courseOfferings;
                
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error loading course offerings";
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLOResultsByCourseOffering(string courseOfferingId)
        {
            try
            {
                var result = await _loResultService.GetLOResultsByCourseOfferingAsync(courseOfferingId);
                return Json(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Get LOResults Failed" });
            }
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
