using System.ComponentModel.DataAnnotations;

namespace LOTA.Model.DTO
{
    public class DeleteSelectedDTO
    {
        [Required(ErrorMessage = "IDs list is required")]
        public List<string> Ids { get; set; } = new List<string>();
    }
}
