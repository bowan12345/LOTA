using LOTA.Model;
using LOTA.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LOTA.Model.DTO.Admin;
using LOTA.Utility;
using LOTA.Model.DTO;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class QualificationController : Controller
    {

        private readonly IQualificationService _qualificationService;

        public QualificationController(IQualificationService qualificationService)
        {
            _qualificationService = qualificationService;
        }

        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Get all qualifications
        /// </summary>
        /// <returns>JSON result with qualifications list</returns>
        [HttpGet]
        public async Task<IActionResult> GetQualifications()
        {
            try
            {
                var qualifications = await _qualificationService.GetAllQualificationsAsync();
                return Json(new { success = true, data = qualifications });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get qualification by ID
        /// </summary>
        /// <param name="id">Qualification ID</param>
        /// <returns>JSON result with qualification details</returns>
        [HttpGet]
        public async Task<IActionResult> GetQualification(string id)
        {
            try
            {
                var qualification = await _qualificationService.GetQualificationByIdAsync(id);
                if (qualification == null)
                {
                    return Json(new { success = false, message = "Qualification not found" });
                }

                return Json(new { success = true, data = qualification });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Create new qualification
        /// </summary>
        /// <param name="qualification">Qualification data</param>
        /// <returns>JSON result</returns>
        [HttpPost]
        public async Task<IActionResult> CreateQualification([FromBody] QualificationCreateDTO qualification)
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

                //check level range
                if (qualification.QualificationType.Replace(" ", "") == QualificationTypeEnum.Certificate.ToString() && (qualification.Level < 1 || qualification.Level > 6))
                {
                    return Json(new { success = false, message = " Certificate Level between 1 and 6" });
                }
                if (qualification.QualificationType.Replace(" ", "") == QualificationTypeEnum.Diploma.ToString() && (qualification.Level < 4 || qualification.Level > 7))
                {
                    return Json(new { success = false, message = " Diploma Level between 4 and 7" });
                }
                if (qualification.QualificationType.Replace(" ", "") == QualificationTypeEnum.Bachelor.ToString() && (qualification.Level < 5 || qualification.Level > 7))
                {
                    return Json(new { success = false, message = " Bachelor Level between 5 and 7" });
                }
                if (qualification.QualificationType.Replace(" ", "") == QualificationTypeEnum.GraduateDiploma.ToString() && qualification.Level != 7)
                {
                    return Json(new { success = false, message = " GraduateDiploma  Level is 7" });
                }
                if (qualification.QualificationType.Replace(" ", "") == QualificationTypeEnum.PostgraduateCertificate.ToString() && qualification.Level != 8)
                {
                    return Json(new { success = false, message = " PostgraduateCertificate  Level is 8" });
                }
                if (qualification.QualificationType.Replace(" ", "") == QualificationTypeEnum.Master.ToString() && (qualification.Level < 8 || qualification.Level > 9))
                {
                    return Json(new { success = false, message = " Master  Level between 8 and 9" });
                }
                if (qualification.QualificationType.Replace(" ", "") == QualificationTypeEnum.PhD.ToString() && qualification.Level != 10)
                {
                    return Json(new { success = false, message = " PhD  Level is 10" });
                }

                var result = await _qualificationService.CreateQualificationAsync(qualification);
                return Json(new { success = true, data = result, message = "Qualification created successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while creating the qualification" });
            }
        }

        /// <summary>
        /// Update qualification
        /// </summary>
        /// <param name="qualification">Updated qualification data</param>
        /// <returns>JSON result</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateQualification([FromBody] QualificationUpdateDTO qualification)
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

                //check level range
                if (qualification.QualificationType.Replace(" ", "") == QualificationTypeEnum.Certificate.ToString() && (qualification.Level < 1 || qualification.Level > 6))
                {
                    return Json(new { success = false, message = " Certificate Level between 1 and 6" });
                }
                if (qualification.QualificationType.Replace(" ", "") == QualificationTypeEnum.Diploma.ToString() && (qualification.Level < 4 || qualification.Level > 7))
                {
                    return Json(new { success = false, message = " Diploma Level between 4 and 7" });
                }
                if (qualification.QualificationType.Replace(" ", "") == QualificationTypeEnum.Bachelor.ToString() && (qualification.Level < 5 || qualification.Level > 7))
                {
                    return Json(new { success = false, message = " Bachelor Level between 5 and 7" });
                }
                if (qualification.QualificationType.Replace(" ", "") == QualificationTypeEnum.GraduateDiploma.ToString() && qualification.Level != 7)
                {
                    return Json(new { success = false, message = " GraduateDiploma  Level is 7" });
                }
                if (qualification.QualificationType.Replace(" ", "") == QualificationTypeEnum.PostgraduateCertificate.ToString() && qualification.Level != 8)
                {
                    return Json(new { success = false, message = " PostgraduateCertificate  Level is 8" });
                }
                if (qualification.QualificationType.Replace(" ", "") == QualificationTypeEnum.Master.ToString() && (qualification.Level < 8 || qualification.Level > 9))
                {
                    return Json(new { success = false, message = " Master  Level between 8 and 9" });
                }
                if (qualification.QualificationType.Replace(" ", "") == QualificationTypeEnum.PhD.ToString() && qualification.Level != 10)
                {
                    return Json(new { success = false, message = " PhD  Level is 10" });
                }

                var result = await _qualificationService.UpdateQualificationAsync(qualification);
                return Json(new { success = true, data = result, message = "Qualification updated successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while updating the qualification" });
            }
        }

        /// <summary>
        /// Delete qualification
        /// </summary>
        /// <param name="id">Qualification ID</param>
        /// <returns>JSON result</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteQualification(string id)
        {
            try
            {
                var result = await _qualificationService.DeleteQualificationAsync(id);
                if (!result)
                {
                    return Json(new { success = false, message = "Qualification not found" });
                }

                return Json(new { success = true, message = "Qualification deleted successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting the qualification" });
            }
        }

        /// <summary>
        /// Delete multiple qualifications
        /// </summary>
        /// <param name="request">Request containing list of qualification IDs</param>
        /// <returns>JSON result</returns>
        [HttpPost]
        public async Task<IActionResult> DeleteSelected([FromBody] DeleteSelectedDTO request)
        {
            try
            {
                if (request?.Ids == null || !request.Ids.Any())
                {
                    return Json(new { success = false, message = "No qualifications selected for deletion" });
                }

                var deletedCount = 0;
                var errors = new List<string>();

                foreach (var id in request.Ids)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(id))
                        {
                            errors.Add("Qualification ID is required");
                            continue;
                        }

                        var result = await _qualificationService.DeleteQualificationAsync(id);
                        if (result)
                        {
                            deletedCount++;
                        }
                        else
                        {
                            errors.Add($"Qualification with ID {id} not found");
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Failed to delete qualification {id}: {ex.Message}");
                    }
                }

                if (deletedCount > 0)
                {
                    var message = $"Successfully deleted {deletedCount} qualification(s)";
                    if (errors.Any())
                    {
                        message += $". {errors.Count} error(s) occurred: {string.Join("; ", errors)}";
                    }
                    return Json(new { success = true, message = message, deletedCount, errorCount = errors.Count });
                }
                else
                {
                    return Json(new { success = false, message = "No qualifications were deleted. Errors: " + string.Join("; ", errors) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred during batch deletion. Please try again or contact support." });
            }
        }

        /// <summary>
        /// Check if qualification name exists
        /// </summary>
        /// <param name="name">Qualification name</param>
        /// <param name="excludeId">ID to exclude from check</param>
        /// <returns>JSON result</returns>
        [HttpGet]
        public async Task<IActionResult> CheckQualificationName(string name, string? excludeId = null)
        {
            try
            {
                var exists = await _qualificationService.IsQualificationNameExistsAsync(name, excludeId);
                return Json(new { success = true, exists });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get all qualification types from database
        /// </summary>
        /// <returns>JSON result with qualification types list</returns>
        [HttpGet]
        public async Task<IActionResult> GetQualificationTypes()
        {
            try
            {
                var qualificationTypes = await _qualificationService.GetAllQualificationTypesAsync();
                return Json(new { success = true, data = qualificationTypes });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
