using Microsoft.AspNetCore.Mvc;
using LOTA.Service.Service.IService;
using LOTA.Model.DTO.Admin;
using System.Text.Json;
using LOTA.Service.Service;
using LOTA.Utility;
using Microsoft.AspNetCore.Authorization;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area(Roles.Role_Admin)]
    [Authorize(Roles = Roles.Role_Student)]
    public class LOResultController : Controller
    {
        private readonly ILOResultService _loResultService;
        private readonly ITrimesterCourseService _trimesterCourseService;

        public LOResultController(ILOResultService loResultService, ITrimesterCourseService trimesterCourseService)
        {
            _loResultService = loResultService;
            _trimesterCourseService = trimesterCourseService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var courseOfferings = await _trimesterCourseService.GetLatestTrimesterCourseOfferingsAsync();
                return View(courseOfferings);
            }
            catch (Exception ex)
            {
                return View(new List<CourseOfferingReturnDTO>());
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
