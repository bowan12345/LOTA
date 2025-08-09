namespace LOTA.Model.DTO.Admin
{
    public class StudentReturnDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? StudentNo { get; set; }
        public bool IsActive { get; set; }
        public int EnrolledCoursesCount { get; set; } = 0;
    }
}
