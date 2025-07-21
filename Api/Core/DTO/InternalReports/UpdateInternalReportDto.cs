using System;

namespace Core.DTO.InternalReports
{
    public class UpdateInternalReportDto
    {
        public string? Title { get; set; }
        public int? ProfessionalId { get; set; }
        public int? TeamId { get; set; }
        public string? Category { get; set; }
        public string? Status { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string? Priority { get; set; }
        public int? AssignedToId { get; set; }
    }
}
