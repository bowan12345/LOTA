using LOTA.Model;
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

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid data provided" });
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
        public async Task<IActionResult> Update([FromBody] UpdateTutorRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Invalid data provided" });
                }

                var tutor = await _userManager.FindByIdAsync(request.Id);
                if (tutor == null)
                {
                    return Json(new { success = false, message = "Tutor not found" });
                }

                // Update tutor properties
                tutor.FirstName = request.FirstName;
                tutor.LastName = request.LastName;
                tutor.TutorNo = request.TutorNo;
                tutor.Email = request.Email;
                tutor.UserName = request.Email;
                tutor.IsActive = request.IsActive;

                // Update password if provided
                if (!string.IsNullOrEmpty(request.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(tutor);
                    var result = await _userManager.ResetPasswordAsync(tutor, token, request.Password);
                    if (!result.Succeeded)
                    {
                        return Json(new { success = false, message = "Failed to update password" });
                    }
                }

                var updateResult = await _userManager.UpdateAsync(tutor);
                if (updateResult.Succeeded)
                {
                    // TODO: Handle course assignments if needed
                    // This would involve updating TutorCourse relationships
                    
                    return Json(new { success = true, message = "Tutor updated successfully" });
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
                return Json(new { success = false, message = "An error occurred while deleting the tutor" });
            }
        }
    }
} 