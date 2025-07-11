namespace Core.DTO.Materials
{
    public class MaterialFiltersDto
    {
        public string? Category { get; set; }
        public string? Status { get; set; }
        public string? Supplier { get; set; }
        public bool? LowStock { get; set; }
        public bool? Expiring { get; set; }
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
