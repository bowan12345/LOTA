using LOTA.Model;
using LOTA.Model.DTO;
using LOTA.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using LOTAWeb.Models;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TutorController : Controller
    {
        private readonly ITutorService _tutorService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TutorController(ITutorService tutorService, UserManager<ApplicationUser> userManager)
        {
            _tutorService = tutorService;
            _userManager = userManager;
        }

        // GET: Admin/Tutor home page
        public async Task<IActionResult> Index()
        {
           
            //query all tutor information from tutorservice
            IEnumerable<ApplicationUser> tutorList = await _tutorService.GetAllTutorsAsync();
            //return all tutor information on the home page 
            return View(tutorList);
        }

        // POST: Admin/Tutor/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TutorCreateDTO tutorCreateDTO)
        {
            // Validate required fields for creation
            if (string.IsNullOrEmpty(tutorCreateDTO.Password))
            {
                return Json(new { success = false, message = "Password is required for creating a new tutor" });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return Json(new { success = false, message = "Validation errors: " + string.Join(", ", errors) });
            }

            // Check if email already exists
            var existingUser = await _userManager.FindByEmailAsync(tutorCreateDTO.Email);
            if (existingUser != null)
            {
                return Json(new { success = false, message = "Email already exists" });
            }

            // Create new tutor
            var tutor = new ApplicationUser
            {
                UserName = tutorCreateDTO.Email,
                Email = tutorCreateDTO.Email,
                FirstName = tutorCreateDTO.FirstName,
                LastName = tutorCreateDTO.LastName,
                TutorNo = tutorCreateDTO.Email, // Use email as TutorNo for now
                IsActive = true,
                EmailConfirmed = true
            };

            //save user info
            var result = await _userManager.CreateAsync(tutor, tutorCreateDTO.Password);
            if (result.Succeeded)
            {
                var newTutorId = tutor.Id;
                //save course info
                try
                {
                    await _tutorService.AddTutorCourseAsync(newTutorId, tutorCreateDTO.AssignedCourses);
                    return Json(new { success = true, message = "Tutor created successfully" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Tutor created but failed to assign courses: " + ex.Message });
                }
            }
            else
            {
                return Json(new { success = false, message = string.Join(", ", result.Errors.Select(e => e.Description)) });
            }
           
        }


        // POST: Admin/Tutor/Update
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] TutorUpdateDTO tutorUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return Json(new { success = false, message = "Validation errors: " + string.Join(", ", errors) });
                }

                var tutor = await _userManager.FindByIdAsync(tutorUpdateDTO.Id);
                if (tutor == null)
                {
                    return Json(new { success = false, message = "Tutor not found" });
                }
                // Check if email already exists
                var existingUser = await _userManager.FindByEmailAsync(tutorUpdateDTO.Email);
                if (existingUser != null)
                {
                    return Json(new { success = false, message = "Email already exists" });
                }

                // Update tutor properties
                tutor.FirstName = tutorUpdateDTO.FirstName;
                tutor.LastName = tutorUpdateDTO.LastName;
                tutor.Email = tutorUpdateDTO.Email;
                tutor.TutorNo = tutorUpdateDTO.Email;

                // Update password if provided
                if (!string.IsNullOrEmpty(tutorUpdateDTO.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(tutor);
                    var result = await _userManager.ResetPasswordAsync(tutor, token, tutorUpdateDTO.Password);
                    if (!result.Succeeded)
                    {
                        return Json(new { success = false, message = "Failed to update password" });
                    }
                }

                var updateResult = await _userManager.UpdateAsync(tutor);
                if (updateResult.Succeeded)
                {
                    // Handle course assignments
                    try
                    {
                        // Remove existing course assignments
                        await _tutorService.RemoveAllTutorCoursesAsync(tutorUpdateDTO.Id);
                        
                        // Add new course assignments if provided
                        if (tutorUpdateDTO.AssignedCourses != null && tutorUpdateDTO.AssignedCourses.Any())
                        {
                            await _tutorService.AddTutorCourseAsync(tutorUpdateDTO.Id, tutorUpdateDTO.AssignedCourses);
                        }
                        
                        return Json(new { success = true, message = "Tutor updated successfully" });
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, message = "Tutor updated but failed to update course assignments: " + ex.Message });
                    }
                }
                else
                {
                    return Json(new { success = false, message = string.Join(", ", updateResult.Errors.Select(e => e.Description)) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while updating the tutor" });
            }
        }

        // GET: Admin/Tutor/GetTutorById/{id}
        [HttpGet]
        public async Task<IActionResult> GetTutorById(string id)
        {
            try
            {
                var tutor = await _tutorService.GetTutorByIdAsync(id);
                if (tutor == null)
                {
                    return Json(new { success = false, message = "Tutor not found" });
                }

                // Get assigned courses for this tutor
                var assignedCourses = await _tutorService.GetTutorCoursesAsync(id);
                var courseIds = assignedCourses.Select(tc => tc.CourseId).ToList();

                var tutorData = new
                {
                    id = tutor.Id,
                    firstName = tutor.FirstName,
                    lastName = tutor.LastName,
                    email = tutor.Email,
                    tutorNo = tutor.TutorNo,
                    isActive = tutor.IsActive,
                    assignedCourses = courseIds
                };

                return Json(new { success = true, data = tutorData });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while retrieving the tutor" });
            }
        }

        // DELETE: Admin/Tutor/Delete/{id}
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var tutor = await _userManager.FindByIdAsync(id);
                if (tutor == null)
                {
                    return Json(new { success = false, message = "Tutor not found" });
                }

                // Step 1: Delete all course assignments (sub-table) first
                try
                {
                    await _tutorService.RemoveAllTutorCoursesAsync(id);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Failed to remove course assignments: " + ex.Message });
                }

                // Step 2: Delete the tutor (main table)
                var result = await _userManager.DeleteAsync(tutor);
                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "Tutor deleted successfully" });
                }
                else
                {
                    return Json(new { success = false, message = string.Join(", ", result.Errors.Select(e => e.Description)) });
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error deleting tutor {id}: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                
                return Json(new { success = false, message = "An error occurred while deleting the tutor. Please try again or contact support." });
            }
        }
    }
} 