// Core/DTO/InternalReports/InternalReportFiltersDto.cs
using Core.Enums;
namespace Core.DTO.InternalReports
{
    public class InternalReportFiltersDto
    {
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public string? Category { get; set; }
        public int? ProfessionalId { get; set; }
        public int? TeamId { get; set; }
        public string? Search { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}


