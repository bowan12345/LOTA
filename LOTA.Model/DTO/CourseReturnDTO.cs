using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model.DTO
{
    /// <summary>
    ///  Data Transfer Object of Course 
    ///  range : backend ->  frontend 
    /// </summary>
    public class CourseReturnDTO
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
    }
}
