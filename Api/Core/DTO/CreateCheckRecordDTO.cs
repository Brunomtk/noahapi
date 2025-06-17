using System;

namespace Core.DTO.CheckRecords
{
    public class CreateCheckRecordDTO
    {
        public Guid ProfessionalId { get; set; }
        public string? ProfessionalName { get; set; }

        public Guid CompanyId { get; set; }

        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }

        public Guid AppointmentId { get; set; }

        public string Address { get; set; } = string.Empty;

        public Guid? TeamId { get; set; }
        public string? TeamName { get; set; }

        public string ServiceType { get; set; } = string.Empty;

        public string? Notes { get; set; }
    }
}
