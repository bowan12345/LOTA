using LOTA.Model;
using LOTA.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using LOTAWeb.Models;
using LOTA.Service.Service;
using LOTA.Model.DTO;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: Admin/Tutor home page
        public async Task<IActionResult> Index()
        {

            //query all courseList information from courseservice
            var courseList = await _courseService.GetAllCoursesAsync(null);
            //return all course information on the home page 
            return View(courseList);
        }

        /// <summary>
        ///  to do 
        /// </summary>
        /// <param name="courseQueryDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetALLCouseByConditions([FromBody]CourseQueryDTO courseQueryDTO)
        {

            //query all courseList information from courseservice
            var courseList = await _courseService.GetAllCoursesAsync(courseQueryDTO);
            //return all course information on the home page 
            return Json(new
            {
                success = true,
                data = courseList
            });
        }

        /// <summary>
        ///  search courses by course name or course code
        /// </summary>
        /// <param name="courseSearchItem"> course name or course code </param>
        /// <returns> course list </returns>
        // GET: Admin/Course/SearchCourses
        [HttpGet]
        public async Task<IActionResult> SearchCourses([FromQuery] string courseSearchItem = "")
        {
            try
            {
                var courseList = await _courseService.GetCoursesByNameOrCodeAsync(courseSearchItem);
                return Json(new { success = true, data = courseList });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchCourses: {ex.Message}");
                return Json(new { success = false, message = $"Failed to search courses: {ex.Message}" });
            }
        }


    }
} 