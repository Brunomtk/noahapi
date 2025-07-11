namespace Core.DTO.Materials
{
    public class MaterialOrderFiltersDto
    {
        public int? MaterialId { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public string? RequestedBy { get; set; }
        public string? Supplier { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
