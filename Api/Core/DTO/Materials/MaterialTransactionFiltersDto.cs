namespace Core.DTO.Materials
{
    public class MaterialTransactionFiltersDto
    {
        public int? MaterialId { get; set; }
        public string? Type { get; set; }
        public int? ProfessionalId { get; set; }
        public int? AppointmentId { get; set; }
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
