using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LOTA.Model;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using LOTA.Model.DTO;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(IStudentService studentService, UserManager<ApplicationUser> userManager)
        {
            _studentService = studentService;
            _userManager = userManager;
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

                //var resultDto = await _studentService.GetStudentByIdAsync(student.Id);
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
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                if (student == null)
                {
                    return Json(new { success = false, message = "Student not found" });
                }

                return Json(new { success = true, data = student });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] StudentUpdateDTO studentDto)
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
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while updating the student" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var student = await _userManager.FindByIdAsync(id);
                if (student == null)
                {
                    return Json(new { success = false, message = "Student not found" });
                }

                // Check if student has enrolled courses
                var enrolledCourses = student.StudentCourses?.Count ?? 0;
                if (enrolledCourses > 0)
                {
                    return Json(new { success = false, message = $"Cannot delete student '{student.FirstName} {student.LastName}' because they have enrolled courses." });
                }

                var result = await _userManager.DeleteAsync(student);
                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "Student deleted successfully" });
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return Json(new { success = false, message = $"Failed to delete student: {errors}" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting the student" });
            }
        }

        // POST: Admin/Student/DeleteSelected
        [HttpPost]
        public async Task<IActionResult> DeleteSelected([FromBody] DeleteSelectedDTO request)
        {
            try
            {
                if (request?.Ids == null || !request.Ids.Any())
                {
                    return Json(new { success = false, message = "No students selected for deletion" });
                }

                var deletedCount = 0;
                var errors = new List<string>();

                foreach (var id in request.Ids)
                {
                    try
                    {
                        var student = await _userManager.FindByIdAsync(id);
                        if (student == null)
                        {
                            errors.Add($"Student with ID {id} not found");
                            continue;
                        }

                        // Check if student has enrolled courses,delete all enrolled course
                        var enrolledCourses = student.StudentCourses?.Count ?? 0;
                        if (enrolledCourses > 0)
                        {
                            errors.Add($"Cannot delete student '{student.FirstName} {student.LastName}' because they have enrolled courses.");
                            continue;
                        }

                        var result = await _userManager.DeleteAsync(student);
                        if (result.Succeeded)
                        {
                            deletedCount++;
                        }
                        else
                        {
                            errors.Add($"Failed to delete {student.FirstName} {student.LastName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Error processing student {id}: {ex.Message}");
                    }
                }

                if (deletedCount > 0)
                {
                    var message = $"Successfully deleted {deletedCount} student(s)";
                    if (errors.Any())
                    {
                        message += $". {errors.Count} error(s) occurred: {string.Join("; ", errors)}";
                    }
                    return Json(new { success = true, message = message, deletedCount, errorCount = errors.Count });
                }
                else
                {
                    return Json(new { success = false, message = "No students were deleted. Errors: " + string.Join("; ", errors) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred during batch deletion. Please try again or contact support." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> CheckStudentEmail(string email, string? excludeId = null)
        {
            try
            {
                var exists = await _studentService.IsStudentEmailExistsAsync(email, excludeId);
                return Json(new { success = true, exists });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> CheckStudentNo(string studentNo, string? excludeId = null)
        {
            try
            {
                var exists = await _studentService.IsStudentNoExistsAsync(studentNo, excludeId);
                return Json(new { success = true, exists });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
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
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UploadExcel: {ex.Message}");
                return Json(new { success = false, message = $"Failed to upload file: {ex.Message}" });
            }
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
                return Json(new { success = false, message = $"Failed to generate template: {ex.Message}" });
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

                        // Generate a default password
                        var defaultPassword = "Student123!";

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
                            EmailConfirmed = true
                        };

                        var result = await _userManager.CreateAsync(student, defaultPassword);
                        if (result.Succeeded)
                        {
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
        public async Task<IActionResult> GetEnrolledStudents(string id, int? academicYear = null, int? trimesterNumber = null)
        {
            try
            {
                var students = await _studentService.GetEnrolledStudentsAsync(id, academicYear, trimesterNumber);
                return Json(new { success = true, data = students });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
