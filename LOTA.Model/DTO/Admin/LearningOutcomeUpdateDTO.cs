using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model.DTO.Admin
{
    public class LearningOutcomeUpdateDTO
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "LOName is required")]
        [StringLength(50, ErrorMessage = "LOName cannot exceed 50 characters")]
        public string LOName { get; set; }

        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
        public string Description { get; set; }
    }
}
