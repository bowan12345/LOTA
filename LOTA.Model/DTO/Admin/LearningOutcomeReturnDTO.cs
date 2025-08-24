using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Model.DTO.Admin
{
    public class LearningOutcomeReturnDTO
    {
        public string Id { get; set; }

        public string LOName { get; set; }

        public string Description { get; set; }
    }
}
