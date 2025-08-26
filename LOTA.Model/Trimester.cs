using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model
{
    public class Trimester
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public int AcademicYear { get; set; }

        // stand for tri1, tri2, tri3
        [Required]
        public int TrimesterNumber { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
