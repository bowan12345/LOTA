using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LOTA.DataAccess.Data;
using LOTA.Model;
using System.Linq;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service.IService;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AssessmentController : Controller
    {
        private readonly IAssessmentService _assessmentService;
        private readonly ICourseService _courseService;

        public AssessmentController(IAssessmentService assessmentService, ICourseService courseService)
        {
            _assessmentService = assessmentService;
            _courseService = courseService;
        }

        public async Task<IActionResult> Index([FromQuery] string searchTerm = "")
        {
            IEnumerable<CourseReturnDTO> courseList;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                //query all courseList information by filter
                courseList = await _courseService.GetCoursesByNameOrCodeAsync(searchTerm);
            }
            else
            {
                //query all courseList information from courseservice
                courseList = await _courseService.GetAllCoursesAsync();
            }

            // Pass search term to view for maintaining search state
            ViewBag.SearchTerm = searchTerm;

            //return all course information on the home page 
            return View(courseList);
        }

        // get learning outcomes for a course
        [HttpGet]
        public async Task<IActionResult> GetLearningOutcomes(string courseId)
        {


            return Json(new { success = true, data = courseId });
        }

        //get assessments for a course
        [HttpGet]
        public async Task<IActionResult> GetCourseAssessments(string courseId)
        {
            if (string.IsNullOrEmpty(courseId))
            {
                return BadRequest(new { success = false, message = "Course ID is required" });
            }

           

            return Json(new { success = true, data = courseId });
        }

        //  create a new assessment
        [HttpPost]
        public async Task<IActionResult> CreateAssessment([FromBody] AssessmentCreateDTO request)
        {
            
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Invalid data" });
                }

                // Create new assignment
                var assignment = new Assessment
                {
                    Id = Guid.NewGuid().ToString(),
                    AssessmentName = request.AssessmentName,
                    TotalWeight = request.TotalWeight,
                    TotalScore = request.TotalScore,
                    CourseId = request.CourseId,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = User.Identity?.Name ?? "System"
                };

                return Json(new { success = true, message = "Assessment created successfully" });
           
        }

        //update an assessment
        [HttpPut]
        public async Task<IActionResult> UpdateAssessment([FromBody] AssessmentUpdateDTO request)
        {
           
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Invalid data" });
                }

                

                return Json(new { success = true, message = "Assessment updated successfully" });
           
        }

        // delete an assessment
        [HttpDelete]
        public async Task<IActionResult> DeleteAssessment(string id)
        {
            
                return Json(new { success = true, message = "Assessment deleted successfully" });
           
        }
    }

}
