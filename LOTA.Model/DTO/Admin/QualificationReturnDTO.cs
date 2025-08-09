namespace LOTA.Model.DTO.Admin
{
    public class QualificationReturnDTO
    {
        public string Id { get; set; } = string.Empty;
        public string QualificationName { get; set; } = string.Empty;
        public string QualificationType { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int Level { get; set; }
    }
}
