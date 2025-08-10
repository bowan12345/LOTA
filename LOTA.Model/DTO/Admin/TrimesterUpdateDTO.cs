using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model.DTO.Admin
{
    public class TrimesterUpdateDTO
    {
        [Key]
        public string Id { get; set; }
        public int AcademicYear { get; set; }
        public int TrimesterNumber { get; set; }
        //public bool IsActive { get; set; }
    }
}
