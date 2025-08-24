using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model.DTO.Admin
{
    public class AssessmentUpdateDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Assessment name is required")]
        [StringLength(50, ErrorMessage = "Assessment name cannot exceed 50 characters")]
        public string AssessmentName { get; set; }
        public string AssessmentTypeId { get; set; }

        [Required(ErrorMessage = "Course is required")]
        public string CourseOfferingId { get; set; }

        [Required(ErrorMessage = "Trimester is required")]
        public string TrimesterId { get; set; }

        [Required(ErrorMessage = "Weight percentage is required")]
        public decimal TotalWeight { get; set; }
        [Required(ErrorMessage = "TotalScore is required")]
        public decimal TotalScore { get; set; }
        public List<string> LearningOutcomes { get; set; }
    }
}
