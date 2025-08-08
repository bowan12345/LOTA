using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model.DTO.Admin
{
    public class CourseUpdateDTO
    {
        [Required(ErrorMessage = "Course ID is required")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        [StringLength(50, ErrorMessage = "Course name cannot exceed 50 characters")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Course code is required")]
        [StringLength(50, ErrorMessage = "Course code cannot exceed 50 characters")]
        public string CourseCode { get; set; }

        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
        public string Description { get; set; }

        public ICollection<LearningOutcomeUpdateDTO> LearningOutcomes { get; set; } = new List<LearningOutcomeUpdateDTO>();

    }
}
