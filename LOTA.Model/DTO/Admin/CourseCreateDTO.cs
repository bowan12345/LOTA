using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model.DTO.Admin
{
    public class CourseCreateDTO
    {

        [Required(ErrorMessage = "Course name is required")]
        [StringLength(100, ErrorMessage = "Course name cannot exceed 100 characters")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Course code is required")]
        [StringLength(50, ErrorMessage = "Course code cannot exceed 50 characters")]
        public string CourseCode { get; set; }



        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Qualification is required")]
        public string QualificationId { get; set; }

        public ICollection<LearningOutcomeCreateDTO> LearningOutcomes { get; set; } = new List<LearningOutcomeCreateDTO>();
    }


}
