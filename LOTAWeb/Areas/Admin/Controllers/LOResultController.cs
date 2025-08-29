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
                TempData["Error"] = $"Error loading course offerings: {ex.Message}";
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLOResultsByCourseOffering(string courseOfferingId)
        {
            try
            {
                // Add debug logging
                System.Diagnostics.Debug.WriteLine($"Getting LO results for course offering: {courseOfferingId}");
                
                var result = await _loResultService.GetLOResultsByCourseOfferingAsync(courseOfferingId);
                return Json(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                // Add detailed error logging
                System.Diagnostics.Debug.WriteLine($"Error in GetLOResultsByCourseOffering: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRetakeScores([FromBody] RetakeRequestDTO retakeRequest)
        {
            try
            {
                var result = await _loResultService.UpdateRetakeScoresAsync(retakeRequest);
                return Json(new { success = true, message = "Retake scores updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
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

        [HttpGet]
        public async Task<IActionResult> DebugCourseData(string courseOfferingId)
        {
            try
            {
                // Debug method to check course and qualification data
                var courseOffering = await _loResultService.GetLatestTrimesterCourseOfferingsAsync();
                var targetCourse = courseOffering.FirstOrDefault(c => c.Id == courseOfferingId);
                
                if (targetCourse != null)
                {
                    return Json(new { 
                        success = true, 
                        courseId = targetCourse.CourseId,
                        courseName = targetCourse.Course?.CourseName,
                        courseCode = targetCourse.Course?.CourseCode,
                        qualificationId = targetCourse.Course?.QualificationId,
                        qualificationName = targetCourse.Course?.Qualification?.QualificationName,
                        hasQualification = targetCourse.Course?.Qualification != null
                    });
                }
                else
                {
                    return Json(new { success = false, message = "Course offering not found" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
