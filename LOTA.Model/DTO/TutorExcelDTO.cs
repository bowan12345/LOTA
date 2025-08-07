using System.ComponentModel.DataAnnotations;

namespace LOTA.Model.DTO
{
    public class TutorExcelDTO
    {
        [Required]
        public string Surname { get; set; } = string.Empty;
        
        [Required]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        // Optional fields that might be in Excel
        public string? Password { get; set; } 
    }
} 