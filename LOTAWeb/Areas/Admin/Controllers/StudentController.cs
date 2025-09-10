using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using LOTA.Model;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using LOTA.Model.DTO;
using LOTA.Utility;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area(Roles.Role_Admin)]
    [Authorize(Roles = Roles.Role_Admin)]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILOTAEmailSender _emailSender;

        public StudentController(IStudentService studentService, UserManager<ApplicationUser> userManager, ILOTAEmailSender emailSender)
        {
            _studentService = studentService;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            ViewBag.SearchTerm = searchTerm;
            
            var students = await _studentService.GetStudentsByNameOrEmailAsync(searchTerm);
            
            return View(students);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateDTO studentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return Json(new { success = false, message = "Validation failed", errors });
                }

                // Check if email already exists
                if (await _userManager.FindByEmailAsync(studentDto.Email) != null)
                {
                    return Json(new { success = false, message = $"User with email '{studentDto.Email}' already exists." });
                }

                // Check if student number already exists
                if (!string.IsNullOrEmpty(studentDto.StudentNo))
                {
                    var existingStudent = _userManager.Users.FirstOrDefault(u => u.StudentNo == studentDto.StudentNo);
                    if (existingStudent != null)
                    {
                        return Json(new { success = false, message = $"Student with number '{studentDto.StudentNo}' already exists." });
                    }
                }

                var student = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = studentDto.FirstName,
                    LastName = studentDto.LastName,
                    Email = studentDto.Email,
                    UserName = studentDto.Email,
                    PasswordHash = studentDto.Password,
                    StudentNo = studentDto.StudentNo,
                    IsActive = studentDto.IsActive,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(student, studentDto.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return Json(new { success = false, message = $"Failed to create student: {errors}" });
                }
                //add student role
                _userManager.AddToRoleAsync(student, Roles.Role_Student).GetAwaiter().GetResult();
                return Json(new { success = true, data = result, message = "Student created successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while creating the student" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentById(string id)
        {
             var student = await _studentService.GetStudentByIdAsync(id);
                if (student == null)
                {
                    return Json(new { success = false, message = "Student not found" });
                }

                return Json(new { success = true, data = student });
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] StudentUpdateDTO studentDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return Json(new { success = false, message = "Validation failed" +string.Join(", ", errors) });
            }
            var existingStudent = await _userManager.FindByIdAsync(studentDto.Id);
            if (existingStudent == null)
            {
                return Json(new { success = false, message = $"Student with ID '{studentDto.Id}' not found." });
            }

            // Check if email already exists (excluding current student)
            var existingUserWithEmail = await _userManager.FindByEmailAsync(studentDto.Email);
            if (existingUserWithEmail != null && existingUserWithEmail.Id != studentDto.Id)
            {
                return Json(new { success = false, message = $"User with email '{studentDto.Email}' already exists." });
            }

            // Check if student number already exists (excluding current student)
            if (!string.IsNullOrEmpty(studentDto.StudentNo))
            {
                var existingStudentWithNo = _userManager.Users.FirstOrDefault(u => u.StudentNo == studentDto.StudentNo && u.Id != studentDto.Id);
                if (existingStudentWithNo != null)
                {
                    return Json(new { success = false, message = $"Student with number '{studentDto.StudentNo}' already exists." });
                }
            }

            existingStudent.FirstName = studentDto.FirstName;
            existingStudent.LastName = studentDto.LastName;
            existingStudent.Email = studentDto.Email;
            existingStudent.UserName = studentDto.Email;
            existingStudent.StudentNo = studentDto.StudentNo;
            existingStudent.IsActive = studentDto.IsActive;

            // Update password if provided
            if (!string.IsNullOrEmpty(studentDto.Password))
            {
                // Validate password confirmation
                if (string.IsNullOrEmpty(studentDto.ConfirmPassword))
                {
                    return Json(new { success = false, message = "Confirm Password is required when updating password." });
                }

                if (studentDto.Password != studentDto.ConfirmPassword)
                {
                    return Json(new { success = false, message = "Password and Confirm Password do not match." });
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(existingStudent);
                var passwordResult = await _userManager.ResetPasswordAsync(existingStudent, token, studentDto.Password);
                if (!passwordResult.Succeeded)
                {
                    var errors = string.Join(", ", passwordResult.Errors.Select(e => e.Description));
                    return Json(new { success = false, message = $"Failed to update password: {errors}" });
                }
            }

            var result = await _userManager.UpdateAsync(existingStudent);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Json(new { success = false, message = $"Failed to update student: {errors}" });
            }

            var resultDto = await _studentService.GetStudentByIdAsync(existingStudent.Id);
            return Json(new { success = true, data = resultDto, message = "Student updated successfully" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _studentService.DeleteStudentAsync(id);
                if (result)
                {
                    return Json(new { success = true, message = "Student deleted successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Student not found" });
                }
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Failed to delete student: {ex.Message}" });
            }
        }

        // POST: Admin/Student/DeleteSelected
        [HttpPost]
        public async Task<IActionResult> DeleteSelected([FromBody] DeleteSelectedDTO request)
        {
            if (request?.Ids == null || !request.Ids.Any())
            {
                return Json(new { success = false, message = "No students selected for deletion" });
            }

            try
            {
                var (deletedCount, errors) = await _studentService.DeleteStudentsAsync(request.Ids);
                
                if (deletedCount > 0)
                {
                    var message = errors.Any() 
                        ? $"Successfully deleted {deletedCount} students. Some errors occurred: {string.Join(", ", errors)}"
                        : $"Successfully deleted {deletedCount} students.";
                    
                    return Json(new { success = true, message = message, deletedCount = deletedCount });
                }
                else
                {
                    return Json(new { success = false, message = "No students were deleted", errors = errors });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Failed to delete students: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> CheckStudentEmail(string email, string? excludeId = null)
        {
           
            var exists = await _studentService.IsStudentEmailExistsAsync(email, excludeId);
            return Json(new { success = true, exists });
           
        }

        [HttpGet]
        public async Task<IActionResult> CheckStudentNo(string studentNo, string? excludeId = null)
        {
            var exists = await _studentService.IsStudentNoExistsAsync(studentNo, excludeId);
            return Json(new { success = true, exists });
            
        }

        [HttpGet]
        public async Task<IActionResult> SearchStudents(string searchTerm)
        {
            try
            {
                var students = await _studentService.GetStudentsByNameOrEmailAsync(searchTerm);
                return Json(new { success = true, data = students });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile excelFile)
        {
           
            if (excelFile == null || excelFile.Length == 0)
            {
                return Json(new { success = false, message = "Please select a file to upload" });
            }

            // Validate file extension
            var allowedExtensions = new[] { ".xlsx", ".xls" };
            var fileExtension = Path.GetExtension(excelFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return Json(new { success = false, message = "Please select a valid Excel file (.xlsx or .xls)" });
            }

            // Validate file size (max 10MB)
            if (excelFile.Length > 10 * 1024 * 1024)
            {
                return Json(new { success = false, message = "File size must be less than 10MB" });
            }

            using var stream = excelFile.OpenReadStream();
            var workbook = new XLWorkbook(stream);
            // Get first worksheet
            var worksheet = workbook.Worksheet(1);
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
            var requiredHeaders = new[] { "StudentID", "SurName", "FirstName", "Email" };
            var missingHeaders = requiredHeaders.Where(h => !headers.Any(header =>
                string.Equals(header, h, StringComparison.OrdinalIgnoreCase))).ToList();

            if (missingHeaders.Any())
            {
                return Json(new { success = false, message = $"Missing required headers: {string.Join(", ", missingHeaders)}" });
            }
            // Import students
            var (successCount, errors) = await ImportStudentsFromExcelAsync(stream);

            var message = $"Successfully imported {successCount} students.";
            if (errors.Count > 0)
            {
                message += $" {errors.Count} errors occurred during import.";
            }

            return Json(new { 
                success = true, 
                message = message,
                data = new { 
                    successCount, 
                    errorCount = errors.Count,
                    // Return first 10 errors
                    errors = errors.Take(10).ToList() 
                }
            });
            
        }

        [HttpGet]
        public IActionResult DownloadTemplate()
        {
            try
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Students");

                // Add headers
                worksheet.Cell("A1").Value = "StudentID";
                worksheet.Cell("B1").Value = "FirstName";
                worksheet.Cell("C1").Value = "SurName";
                worksheet.Cell("D1").Value = "Email";

                // Style headers
                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;

                // Add sample data
                worksheet.Cell("A2").Value = "STU001";
                worksheet.Cell("B2").Value = "Doe";
                worksheet.Cell("C2").Value = "John";
                worksheet.Cell("D2").Value = "john.doe@example.com";

                worksheet.Cell("A3").Value = "STU002";
                // Optional First Name
                worksheet.Cell("B3").Value = "";
                // Optional Last Name
                worksheet.Cell("C3").Value = "";
                worksheet.Cell("D3").Value = "jane.smith@example.com";

                // Auto-fit columns
                worksheet.Columns().AdjustToContents();

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StudentImportTemplate.xlsx");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DownloadTemplate: {ex.Message}");
                return Json(new { success = false, message = $"Failed to generate template" });
            }
        }

        private async Task<(int successCount, List<string> errors)> ImportStudentsFromExcelAsync(Stream fileStream)
        {
            var errors = new List<string>();
            var successCount = 0;

            try
            {
                using var workbook = new XLWorkbook(fileStream);
                // Get first worksheet
                var worksheet = workbook.Worksheet(1);
                // Skip header row
                var rows = worksheet.RowsUsed().Skip(1);
                foreach (var row in rows)
                {
                    try
                    {
                        var studentNo = row.Cell("A").Value.ToString()?.Trim();
                        var lastName = row.Cell("B").Value.ToString()?.Trim();
                        var firstName = row.Cell("C").Value.ToString()?.Trim();
                        var email = row.Cell("D").Value.ToString()?.Trim();

                        // Validate required fields
                        if (string.IsNullOrWhiteSpace(email))
                        {
                            errors.Add($"Row {row.RowNumber()}: Email is required");
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(studentNo))
                        {
                            errors.Add($"Row {row.RowNumber()}: Student ID is required");
                            continue;
                        }

                        // Generate a default password using password generator
                        var defaultPassword = DefaultPasswords.GetStudentDefaultPassword();

                        // Check if email already exists
                        var existingUser = await _userManager.FindByEmailAsync(email);
                        if (existingUser != null)
                        {
                            errors.Add($"Row {row.RowNumber()}: User with email '{email}' already exists");
                            continue;
                        }

                        // Check if student number already exists
                        if (!string.IsNullOrWhiteSpace(studentNo))
                        {
                            var existingStudent = _userManager.Users.FirstOrDefault(u => u.StudentNo == studentNo);
                            if (existingStudent != null)
                            {
                                errors.Add($"Row {row.RowNumber()}: Student with number '{studentNo}' already exists");
                                continue;
                            }
                        }

                        // Create student
                        var student = new ApplicationUser
                        {
                            Id = Guid.NewGuid().ToString(),
                            FirstName = firstName,
                            LastName = lastName,
                            Email = email,
                            UserName = email,
                            StudentNo = studentNo,
                            IsActive = true,
                            EmailConfirmed = true,
                            MustChangePassword = DefaultPasswords.ForcePasswordChangeOnFirstLogin
                        };

                        var result = await _userManager.CreateAsync(student, defaultPassword);
                        if (result.Succeeded)
                        {
                            //add student role
                            _userManager.AddToRoleAsync(student, Roles.Role_Student).GetAwaiter().GetResult();
                            
                            // Send account creation email
                            await _emailSender.SendAccountCreationEmailAsync(student, defaultPassword, Roles.Role_Student);
                            
                            successCount++;
                        }
                        else
                        {
                            var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                            errors.Add($"Row {row.RowNumber()}: Failed to create student - {errorMessages}");
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Row {row.RowNumber()}: Error processing row - {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                errors.Add($"Error reading Excel file: {ex.Message}");
            }

            return (successCount, errors);
        }


        [HttpGet]
        public async Task<IActionResult> GetEnrolledStudents(string courseOfferingId, string trimesterId)
        {
            try
            {
                var students = await _studentService.GetEnrolledStudentsAsync(courseOfferingId, trimesterId);
                return Json(new { success = true, data = students });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
