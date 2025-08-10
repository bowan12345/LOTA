using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model.DTO.Admin
{
    public class TrimesterCreateDTO
    {
        public int AcademicYear { get; set; }
        public int TrimesterNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
