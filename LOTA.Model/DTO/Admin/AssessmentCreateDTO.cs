using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model.DTO.Admin
{
    public class AssessmentCreateDTO
    {

        [Required(ErrorMessage = "Assessment name is required")]
        [StringLength(50, ErrorMessage = "Assessment name cannot exceed 50 characters")]
        public string AssessmentName { get; set; }

        [Required(ErrorMessage = "Assessment Type is required")]
        public string AssessmentTypeId { get; set; }

        [Required(ErrorMessage = "Course is required")]
        public string CourseOfferingId { get; set; }

        [Required(ErrorMessage = "Trimester is required")]
        public string TrimesterId { get; set; }

        [Required(ErrorMessage = "Weight percentage is required")]
        public decimal Weight { get; set; }
        [Required(ErrorMessage = "Score is required")]
        public decimal Score { get; set; }

        [Required(ErrorMessage = "LearningOutcomes is required")]
        public List<AssessmentLearningOutcomeCreateDTO> LearningOutcomes { get; set; }
    }

    public class AssessmentLearningOutcomeCreateDTO
    {
        public string LOId { get; set; }

        public decimal Score { get; set; }
    }

}
