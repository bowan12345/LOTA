using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model
{
    public class ApplicationUser: IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(255)]
        public string? TutorNo { get; set; }

        [MaxLength(255)]
        public string? StudentNo { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<StudentAssessmentScore> StudentScores { get; set; } = new List<StudentAssessmentScore>();
        public ICollection<TutorCourse> TutorCourse { get; set; } = new List<TutorCourse>();
        public ICollection<TrimesterCourse> TrimesterCourse { get; set; } = new List<TrimesterCourse>();
    }
}
