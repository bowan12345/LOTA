using Microsoft.AspNetCore.Mvc;
using LOTA.Service.Service.IService;
using LOTA.Model;

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
    }
}
