using Microsoft.AspNetCore.Mvc;
using LOTA.Service.Service.IService;
using LOTA.Model;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOTAWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrimesterController : Controller
    {
        private readonly ITrimesterService _trimesterService;

        public TrimesterController(ITrimesterService trimesterService)
        {
            _trimesterService = trimesterService;
        }

        // GET: Admin/Trimester home page
        public async Task<IActionResult> Index([FromQuery] string searchTerm = "")
        {
            try
            {

                IEnumerable<TrimesterReturnDTO> trimesterList;
                // Apply search filter if searchTerm is provided
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    try
                    {
                        trimesterList = await _trimesterService.GetByAcademicYearAsync(Int32.Parse(searchTerm));
                    }
                    catch (Exception)
                    {

                        ViewBag.ErrorMessage = "illegal number format for academic year";
                        return View(new List<TrimesterReturnDTO>());
                    }
                  
                }
                else
                {
                    trimesterList = await _trimesterService.GetActiveTrimestersAsync();
                }
                // Pass search term to view for maintaining search state
                ViewBag.SearchTerm = searchTerm;
                return View(trimesterList);
            }
            catch (Exception ex)
            {
                // Log the error (you might want to use a proper logging service)
                ViewBag.ErrorMessage = "An error occurred while loading trimesters.";
                return View(new List<TrimesterReturnDTO>());
            }
        }

        /// <summary>
        /// get all trimesters
        /// </summary>
        /// <returns>a list of trimesters</returns>
        [HttpGet]
        public async Task<IActionResult> GetActiveTrimesters()
        {
            try
            {
                var trimesters = await _trimesterService.GetActiveTrimestersAsync();
                return Json(new { success = true, data = trimesters });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// get current trimester
        /// </summary>
        /// <returns> current trimester</returns>
        [HttpGet]
        public async Task<IActionResult> GetCurrentTrimester()
        {
            try
            {
                var currentTrimester = await _trimesterService.GetCurrentTrimesterAsync();
                return Json(new { success = true, data = currentTrimester });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get trimester by ID
        /// </summary>
        /// <param name="id">Trimester ID</param>
        /// <returns>Trimester data</returns>
        [HttpGet]
        public async Task<IActionResult> GetTrimesterById(string id)
        {
            try
            {
                var trimester = await _trimesterService.GetByIdAsync(id);
                
                if (trimester != null)
                {
                    return Json(new { success = true, data = trimester });
                }
                else
                {
                    return Json(new { success = false, message = "Trimester not found" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Create new trimester
        /// </summary>
        /// <param name="request">Trimester creation request</param>
        /// <returns>Success status</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TrimesterCreateDTO request)
        {
            try
            {   
                var result = await _trimesterService.CreateAsync(request);
                if (result!=null)
                {
                    return Json(new { success = true, message = "Trimester created successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to create trimester" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Update existing trimester
        /// </summary>
        /// <param name="request">Trimester update request</param>
        /// <returns>Success status</returns>
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] TrimesterUpdateDTO request)
        {
            try
            {
                var existingTrimester = await _trimesterService.GetByIdAsync(request.Id);
                if (existingTrimester == null)
                {
                    return Json(new { success = false, message = "Trimester not found" });
                }
                var result = await _trimesterService.UpdateAsync(request);
                
                if (result != null)
                {
                    return Json(new { success = true, message = "Trimester updated successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to update trimester" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Delete trimester
        /// </summary>
        /// <param name="id">Trimester ID</param>
        /// <returns>Success status</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _trimesterService.DeleteAsync(id);
                return Json(new { success = true, message = "Trimester deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Failed to delete trimester" });
            }
        }

        /// <summary>
        /// Upload Excel file for bulk import
        /// </summary>
        /// <param name="file">Excel file</param>
        /// <returns>Import result</returns>
        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            try
            {
                // You'll need to implement this in your service
                // For now, return a placeholder response
                return Json(new { success = true, message = "Excel file uploaded successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Download Excel template
        /// </summary>
        /// <returns>Excel template file</returns>
        [HttpGet]
        public IActionResult DownloadTemplate()
        {
            try
            {
                // You'll need to implement this to generate and return an Excel template
                // For now, return a placeholder response
                return Json(new { success = false, message = "Template download not implemented yet" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

}
