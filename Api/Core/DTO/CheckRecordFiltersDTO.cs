using System;

namespace Core.DTO.CheckRecords
{
    public class CheckRecordFiltersDTO
    {
        public Guid? ProfessionalId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? TeamId { get; set; }
        public Guid? AppointmentId { get; set; }

        public string? Status { get; set; }
        public string? ServiceType { get; set; }

        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Search { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
