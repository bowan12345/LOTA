using Microsoft.AspNetCore.Mvc;
using LOTA.Model;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service.IService;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Utility;
using Microsoft.AspNetCore.Authorization;
namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area(Roles.Role_Admin)]
    [Authorize(Roles = Roles.Role_Admin)]
    public class AssessmentController : Controller
    {
        private readonly IAssessmentService _assessmentService;
        private readonly ICourseService _courseService;
        private readonly ITrimesterCourseService _trimesterCourseService;

        public AssessmentController(IAssessmentService assessmentService, ICourseService courseService, ITrimesterCourseService trimesterCourseService)
        {
            _assessmentService = assessmentService;
            _courseService = courseService;
            _trimesterCourseService = trimesterCourseService;
        }

        public async Task<IActionResult> Index([FromQuery] string searchTerm = "")
        {
            // Get all assessments with course and trimester information
            IEnumerable<AssessmentReturnDTO> assessmentList;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                assessmentList = await _assessmentService.GetAssessmentsBySearchTermAsync(searchTerm);
            }
            else
            {
                assessmentList = await _assessmentService.GetAllAssessmentsAsync();
            }

            // Pass search term to view for maintaining search state
            ViewBag.SearchTerm = searchTerm;

            // Return assessments data to the view
            return View(assessmentList);
        }

        // get learning outcomes for a course
        [HttpGet]
        public async Task<IActionResult> GetLearningOutcomes(string courseOfferingId)
        {
        
            try
            {
                var LOList = await _assessmentService.GetLearningOutcomesByCourseOfferingIdAsync(courseOfferingId);
                return Json(new { success = true, data = LOList });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
           
        }

        // Get all assessment types
        [HttpGet]
        public async Task<IActionResult> GetAssessmentTypes()
        {
            try
            {
                var assessmentTypes = await _assessmentService.GetAllAssessmentTypesAsync();
                return Json(new { success = true, data = assessmentTypes });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Get latest trimester course offerings for assessment forms
        [HttpGet]
        public async Task<IActionResult> GetLatestTrimesterCourseOfferings()
        {
            try
            {
                var courseOfferings = await _trimesterCourseService.GetLatestTrimesterCourseOfferingsAsync();
                return Json(new { success = true, data = courseOfferings });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Get assessment by ID
        [HttpGet]
        public async Task<IActionResult> GetAssessmentById(string id)
        {
            try
            {
                var assessment = await _assessmentService.GetAssessmentByIdAsync(id);
                if (assessment != null)
                {
                    return Json(new { success = true, data = assessment });
                }
                return Json(new { success = false, message = "Assessment not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        //  create a new assessment
        [HttpPost]
        public async Task<IActionResult> CreateAssessment([FromBody] AssessmentCreateDTO request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return Json(new { success = false, message = "Validation errors: " + string.Join(", ", errors) });
            }
            try
            {
                //checks if it has LOs
                if (request.LearningOutcomes.Count <= 0)
                {
                    return Json(new { success = false, message = "Learning outcomes cannot be emplty" });
                }

                // Save to database
                var result = await _assessmentService.CreateAssessmentAsync(request);
                if (result != null)
                {
                    return Json(new { success = true, message = "Assessment created successfully" });
                }
                return Json(new { success = false, message = "Failed to create assessment" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

                //update an assessment
        [HttpPut]
        public async Task<IActionResult> UpdateAssessment([FromBody] AssessmentUpdateDTO request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return Json(new { success = false, message = "Validation errors: " + string.Join(", ", errors) });
            }
            try
            {
                //checks if it has LOs
                if (request.LearningOutcomes.Count <= 0)
                {
                    return Json(new { success = false, message = "Learning outcomes cannot be emplty" });
                }

                // update information
                await _assessmentService.UpdateAssessmentAsync(request);
                return Json(new { success = true, message = "Assessment updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // delete an assessment
        [HttpDelete]
        public async Task<IActionResult> DeleteAssessment(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest(new { success = false, message = "Assessment ID is required" });
                }

                // Delete from database
                 await _assessmentService.DeleteAssessmentAsync(id);
                return Json(new { success = true, message = "Assessment deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

}
