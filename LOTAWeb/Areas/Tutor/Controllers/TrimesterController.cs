using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using LOTA.Service.Service.IService;
using LOTA.Model;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOTA.Model.DTO;
using DocumentFormat.OpenXml.Office2010.Excel;
using LOTA.Utility;
using Microsoft.AspNetCore.Authorization;

namespace LOTAWeb.Areas.Tutor.Controllers
{
    
    [Area(Roles.Role_Tutor)]
    [Authorize(Roles = Roles.Role_Tutor)]
    public class TrimesterController : Controller
    {
        private readonly ITrimesterService _trimesterService;

        public TrimesterController(ITrimesterService trimesterService)
        {
            _trimesterService = trimesterService;
        }

 
        /// <summary>
        /// get latest trimester
        /// </summary>
        /// <returns> current trimester</returns>
        [HttpGet]
        public async Task<IActionResult> GetLatestTrimester()
        {
            try
            {
                var currentTrimester = await _trimesterService.GetLatestTrimesterAsync();
                return Json(new { success = true, data = currentTrimester });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

      
    }

}
