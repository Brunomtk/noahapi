namespace Core.DTO.Teams
{
    public class CreateTeamDTO
    {
        public string Name { get; set; } = string.Empty;
        public int? LeaderId { get; set; }  // nullable para opcional
        public string? Region { get; set; }
        public string? Description { get; set; }
        public int CompanyId { get; set; }  // obrigat�rio
    }
}
