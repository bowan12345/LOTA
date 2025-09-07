using LOTA.Model;
using LOTA.Model.DTO;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using LOTAWeb.Models;
using ClosedXML.Excel;
using System.Text.RegularExpressions;
using LOTA.Service.Service;
using LOTA.Utility;
using Microsoft.AspNetCore.Authorization;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area(Roles.Role_Admin)]
    [Authorize(Roles = Roles.Role_Student)]
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
        public async Task<IActionResult> Index(string searchTerm = "")
        {
            // Query tutor information from tutor service with optional search
            IEnumerable<ApplicationUser> tutorList;
            
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                tutorList = await _tutorService.SearchTutorsAsync(searchTerm);
            }
            else
            {
                tutorList = await _tutorService.GetAllTutorsAsync();
            }
            
            // Pass search term to view for maintaining search state
            ViewBag.SearchTerm = searchTerm;
            
            // Return all tutor information on the home page 
            return View(tutorList);
        }

        [HttpGet]
        public async Task<IActionResult> GetTutors()
        {
            try
            {
                var tutorList = await _tutorService.GetAllTutorsAsync();
                return Json(new { success = true, data = tutorList });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchCourses: {ex.Message}");
                return Json(new { success = false, message = $"Failed to load tutor list: {ex.Message}" });
            }
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
                // Use email as TutorNo for now
                TutorNo = tutorCreateDTO.Email, 
                PasswordHash = tutorCreateDTO.Password,
                IsActive = true,
                EmailConfirmed = true
            };

            //save user info
            var result = await _userManager.CreateAsync(tutor, tutorCreateDTO.Password);
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(tutor, Roles.Role_Tutor).GetAwaiter().GetResult();
                return Json(new { success = true, message = "Tutor created successfully" });
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
                if (existingUser != null && !existingUser.Id.Equals(tutorUpdateDTO.Id))
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
                    // Validate password confirmation
                    if (string.IsNullOrEmpty(tutorUpdateDTO.ConfirmPassword))
                    {
                        return Json(new { success = false, message = "Confirm Password is required when updating password." });
                    }

                    if (tutorUpdateDTO.Password != tutorUpdateDTO.ConfirmPassword)
                    {
                        return Json(new { success = false, message = "Password and Confirm Password do not match." });
                    }

                    var token = await _userManager.GeneratePasswordResetTokenAsync(tutor);
                    var result = await _userManager.ResetPasswordAsync(tutor, token, tutorUpdateDTO.Password);
                    if (!result.Succeeded)
                    {
                        return Json(new { success = false, message = $"Failed to update password" });
                    }
                }

                var updateResult = await _userManager.UpdateAsync(tutor);
                if (updateResult.Succeeded)
                {
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
                    _userManager.RemoveFromRoleAsync(tutor, Roles.Role_Tutor).GetAwaiter().GetResult();
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

        // POST: Admin/Tutor/DeleteSelected
        [HttpPost]
        public async Task<IActionResult> DeleteSelected([FromBody] DeleteSelectedDTO request)
        {
            try
            {
                if (request?.Ids == null || !request.Ids.Any())
                {
                    return Json(new { success = false, message = "No tutors selected for deletion" });
                }

                var deletedCount = 0;
                var errors = new List<string>();

                foreach (var id in request.Ids)
                {
                    try
                    {
                        var tutor = await _userManager.FindByIdAsync(id);
                        if (tutor == null)
                        {
                            errors.Add($"Tutor with ID {id} not found");
                            continue;
                        }

                        // Step 1: Delete all course assignments (sub-table) first
                        try
                        {
                            await _tutorService.RemoveAllTutorCoursesAsync(id);
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Failed to remove course assignments for {tutor.Email}: {ex.Message}");
                            continue;
                        }

                        // Step 2: Delete the tutor (main table)
                        var result = await _userManager.DeleteAsync(tutor);
                        if (result.Succeeded)
                        {
                            _userManager.RemoveFromRoleAsync(tutor, Roles.Role_Tutor).GetAwaiter().GetResult();
                            deletedCount++;
                        }
                        else
                        {
                            errors.Add($"Failed to delete {tutor.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Error processing tutor {id}: {ex.Message}");
                    }
                }

                if (deletedCount > 0)
                {
                    var message = $"Successfully deleted {deletedCount} tutor(s)";
                    if (errors.Any())
                    {
                        message += $". {errors.Count} error(s) occurred: {string.Join("; ", errors)}";
                    }
                    return Json(new { success = true, message = message, deletedCount, errorCount = errors.Count });
                }
                else
                {
                    return Json(new { success = false, message = "No tutors were deleted. Errors: " + string.Join("; ", errors) });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in batch deletion: {ex.Message}");
                return Json(new { success = false, message = "An error occurred during batch deletion. Please try again or contact support." });
            }
        }

        // POST: Admin/Tutor/UploadExcel
        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return Json(new { success = false, message = "Please select a file to upload" });
                }

                // Validate file extension
                var allowedExtensions = new[] { ".xlsx", ".xls", ".csv" };
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return Json(new { success = false, message = "Please upload a valid Excel file (.xlsx or .xls or .csv)" });
                }

                // ClosedXML is free and open source

                var tutors = new List<TutorExcelDTO>();
                var errors = new List<string>();
                var successCount = 0;
                var errorCount = 0;

                using (var stream = file.OpenReadStream())
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        return Json(new { success = false, message = "No worksheet found in the Excel file" });
                    }

                    // Get the used range
                    var usedRange = worksheet.RangeUsed();
                    // At least header + 1 data row
                    if (usedRange == null || usedRange.RowCount() < 2) 
                    {
                        return Json(new { success = false, message = "Excel file is empty or has no data rows" });
                    }

                    // Validate headers
                    var headers = new List<string>();
                    var headerRow = worksheet.Row(1);
                    for (int col = 1; col <= usedRange.ColumnCount(); col++)
                    {
                        var headerValue = headerRow.Cell(col).Value.ToString()?.Trim();
                        if (!string.IsNullOrEmpty(headerValue))
                        {
                            headers.Add(headerValue);
                        }
                    }

                    // Check required headers
                    var requiredHeaders = new[] { "Surname", "FirstName", "Email" };
                    var missingHeaders = requiredHeaders.Where(h => !headers.Any(header => 
                        string.Equals(header, h, StringComparison.OrdinalIgnoreCase))).ToList();

                    if (missingHeaders.Any())
                    {
                        return Json(new { success = false, message = $"Missing required headers: {string.Join(", ", missingHeaders)}" });
                    }

                    // Process data rows
                    for (int row = 2; row <= usedRange.RowCount(); row++)
                    {
                        try
                        {
                            var surname = worksheet.Cell(row, GetColumnIndex(headers, "Surname")).Value.ToString()?.Trim();
                            var firstName = worksheet.Cell(row, GetColumnIndex(headers, "FirstName")).Value.ToString()?.Trim();
                            var email = worksheet.Cell(row, GetColumnIndex(headers, "Email")).Value.ToString()?.Trim();
                            var password = worksheet.Cell(row, GetColumnIndex(headers, "Password")).Value.ToString()?.Trim();

                            // Skip empty rows
                            if (string.IsNullOrWhiteSpace(surname) && string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(email))
                            {
                                continue;
                            }

                            // Validate required fields
                            if (string.IsNullOrWhiteSpace(surname))
                            {
                                errors.Add($"Row {row}: Surname is required");
                                errorCount++;
                                continue;
                            }

                            if (string.IsNullOrWhiteSpace(firstName))
                            {
                                errors.Add($"Row {row}: FirstName is required");
                                errorCount++;
                                continue;
                            }

                            if (string.IsNullOrWhiteSpace(email))
                            {
                                errors.Add($"Row {row}: Email is required");
                                errorCount++;
                                continue;
                            }

                            // Validate email format
                            if (!IsValidEmail(email))
                            {
                                errors.Add($"Row {row}: Invalid email format: {email}");
                                errorCount++;
                                continue;
                            }

                            // Generate password if not provided
                            if (string.IsNullOrWhiteSpace(password))
                            {
                                errors.Add($"Row {row}: Invalid email format: {password}");
                                errorCount++;
                                continue;
                            }

                            tutors.Add(new TutorExcelDTO
                            {
                                Surname = surname,
                                FirstName = firstName,
                                Email = email,
                                Password = password,
                            });
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Row {row}: Error processing row - {ex.Message}");
                            errorCount++;
                        }
                    }
                }

                // Create tutors in database
                foreach (var tutorData in tutors)
                {
                    try
                    {
                        // Check if email already exists
                        var existingUser = await _userManager.FindByEmailAsync(tutorData.Email);
                        if (existingUser != null)
                        {
                            errors.Add($"Email already exists: {tutorData.Email}");
                            errorCount++;
                            continue;
                        }

                        // Create new tutor
                        var tutor = new ApplicationUser
                        {
                            UserName = tutorData.Email,
                            Email = tutorData.Email,
                            FirstName = tutorData.FirstName,
                            LastName = tutorData.Surname,
                            TutorNo = tutorData.Email, 
                            IsActive = true,
                            EmailConfirmed = true
                        };

                        var result = await _userManager.CreateAsync(tutor, tutorData.Password);
                        if (result.Succeeded)
                        {
                            _userManager.AddToRoleAsync(tutor, Roles.Role_Tutor).GetAwaiter().GetResult();
                            successCount++;
                        }
                        else
                        {
                            errors.Add($"Failed to create tutor {tutorData.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                            errorCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Error creating tutor {tutorData.Email}: {ex.Message}");
                        errorCount++;
                    }
                }

                var message = $"Successfully created {successCount} tutors.";
                if (errorCount > 0)
                {
                    message += $" {errorCount} errors occurred.";
                }

                return Json(new 
                { 
                    success = true, 
                    message = message,
                    data = new
                    {
                        successCount,
                        errorCount,
                        errors = errors.Take(10).ToList() // Return first 10 errors
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UploadExcel: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return Json(new { success = false, message = $"An error occurred while processing the Excel file: {ex.Message}" });
            }
        }

        // Helper method to get column index by header name
        private int GetColumnIndex(List<string> headers, string headerName)
        {
            for (int i = 0; i < headers.Count; i++)
            {
                if (string.Equals(headers[i], headerName, StringComparison.OrdinalIgnoreCase))
                {
                    // Excel columns are 1-based
                    return i + 1; 
                }
            }
            // Not found
            return 0; 
        }

        // Helper method to validate email format
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Helper method to generate default password
        private string GenerateDefaultPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }

          // Helper method to get course IDs by course codes
         private async Task<List<string>> GetCourseIdsByCodes(List<string> courseCodes)
         {
             try
             {
                 // to get courses by codes and return their IDs
                 var courseIds = new List<string>();
                 return courseIds;
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"Error getting course IDs by codes: {ex.Message}");
                 return new List<string>();
             }
         }

         // GET: Admin/Tutor/DownloadTemplate
         [HttpGet]
         public IActionResult DownloadTemplate()
         {
             try
             {
                 // ClosedXML is free and open source

                 using (var workbook = new XLWorkbook())
                 {
                     var worksheet = workbook.Worksheets.Add("Tutor Template");

                     // Add headers
                     worksheet.Cell(1, 1).Value = "Surname";
                     worksheet.Cell(1, 2).Value = "FirstName";
                     worksheet.Cell(1, 3).Value = "Email";
                     worksheet.Cell(1, 4).Value = "Password";

                     // Style headers
                     var headerRange = worksheet.Range(1, 1, 1, 4);
                     headerRange.Style.Font.Bold = true;
                     headerRange.Style.Fill.PatternType = XLFillPatternValues.Solid;
                     headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                     headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                     // Add sample data
                     worksheet.Cell(2, 1).Value = "Smith";
                     worksheet.Cell(2, 2).Value = "John";
                     worksheet.Cell(2, 3).Value = "john.smith@example.com";
                     worksheet.Cell(2, 4).Value = "Password123!";

                     worksheet.Cell(3, 1).Value = "Johnson";
                     worksheet.Cell(3, 2).Value = "Jane";
                     worksheet.Cell(3, 3).Value = "jane.johnson@example.com";
                     worksheet.Cell(3, 4).Value = "Password123!";

                     // Auto-fit columns
                     worksheet.Columns().AdjustToContents();

                     // Set column widths
                     worksheet.Column(1).Width = 15; // Surname
                     worksheet.Column(2).Width = 15; // FirstName
                     worksheet.Column(3).Width = 25; // Email
                     worksheet.Column(4).Width = 15; // Password

                     // Create the file stream
                     var stream = new MemoryStream();
                     workbook.SaveAs(stream);
                     stream.Position = 0;

                     // Return the file
                     return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Tutor_Template.xlsx");
                 }
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"Error creating Excel template: {ex.Message}");
                 return Json(new { success = false, message = "Error creating template file" });
             }
         }
    }
} 