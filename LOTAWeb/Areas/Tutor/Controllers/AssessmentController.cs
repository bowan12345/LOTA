using Microsoft.AspNetCore.Mvc;
using LOTA.Model;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service.IService;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Utility;
using Microsoft.AspNetCore.Authorization;
using LOTA.Service.Service;
using Microsoft.AspNetCore.Identity;
namespace LOTAWeb.Areas.Tutor.Controllers
{
    [Area(Roles.Role_Tutor)]
    [Authorize(Roles = Roles.Role_Tutor)]
    public class AssessmentController : Controller
    {
        private readonly IAssessmentService _assessmentService;
        private readonly ICourseService _courseService;
        private readonly ITrimesterCourseService _trimesterCourseService;
        private readonly ITrimesterService _trimesterService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AssessmentController(IAssessmentService assessmentService, ICourseService courseService, 
                    ITrimesterCourseService trimesterCourseService,ITrimesterService trimesterService, UserManager<ApplicationUser> userManager)
        {
            _assessmentService = assessmentService;
            _courseService = courseService;
            _trimesterCourseService = trimesterCourseService;
            _trimesterService = trimesterService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Get current tutor user id
            var currentUserId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(currentUserId))
            {
                return RedirectToAction("Index", "Home", new { area = Roles.Role_Tutor });
            }

            // Fetch assessments from the latest trimester
            IEnumerable<AssessmentReturnDTO> allTutorAssessments = await _assessmentService.GetAllAssessmentsBuTutorIdAsync(currentUserId);
          
            return View(allTutorAssessments);
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
                // Get current user
                var currentUserId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(currentUserId))
                {
                    return Json(new { success = false, message = "User not authenticated" });
                }
                // Get latest trimester
                var latestTrimester = await _trimesterService.GetLatestTrimesterAsync();
                if (latestTrimester == null || string.IsNullOrEmpty(latestTrimester.Id))
                {
                    return Json(new { success = false, message = "Trimester has not existed" });
                }

                // Get course offerings for current tutor in latest trimester
                var tutorTrimesterCourses = await _trimesterCourseService.GetTrimesterCoursesByTutorAndTrimesterAsync(currentUserId, latestTrimester.Id);
                return Json(new { success = true, data = tutorTrimesterCourses });
            }
            catch (Exception ex)
            {
                return Json(new { success = true, data = new List<TrimesterCourseReturnDTO>() });
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
