using Microsoft.AspNetCore.Mvc;
using LOTA.Service.Service.IService;
using LOTA.Model.DTO.Admin;
using Microsoft.AspNetCore.Authorization;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class LOScoreController : Controller
    {
        private readonly ILOScoreService _loScoreService;

        public LOScoreController(ILOScoreService loScoreService)
        {
            _loScoreService = loScoreService;
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
        public async Task<IActionResult> CreateLOScore([FromBody] LOScoreCreateDTO loscoreCreateDTO)
        {
            try
            {
                var result = await _loScoreService.CreateLOScoreAsync(loscoreCreateDTO);
                return Json(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLOScore([FromBody] LOScoreUpdateDTO loscoreUpdateDTO)
        {
            try
            {
                var result = await _loScoreService.UpdateLOScoreAsync(loscoreUpdateDTO);
                return Json(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLOScore(string id)
        {
            try
            {
                var result = await _loScoreService.DeleteLOScoreAsync(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLOScoresByAssessment(string assessmentId)
        {
            try
            {
                var scores = await _loScoreService.GetLOScoresByAssessmentAsync(assessmentId);
                return Json(new { success = true, data = scores });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLOScoresByStudent(string studentId)
        {
            try
            {
                var scores = await _loScoreService.GetLOScoresByStudentAsync(studentId);
                return Json(new { success = true, data = scores });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLOScoresByCourseOffering(string courseOfferingId)
        {
            try
            {
                var scores = await _loScoreService.GetLOScoresByCourseOfferingAsync(courseOfferingId);
                return Json(new { success = true, data = scores });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
