using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model.DTO
{
    /// <summary>
    ///  Data Transfer Object of Course 
    ///  range : frontend  ->  Controller ↔ Service ↔ API
    /// </summary>
    public class CourseQueryDTO
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string CourseItem { get; set; }
        public string TutorId { get; set; }
        public string StudentId { get; set; }

    }
}
