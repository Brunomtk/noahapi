using System;

namespace Core.DTO.CheckRecords
{
    public class CheckRecordDTO
    {
        public int Id { get; set; }

        public Guid ProfessionalId { get; set; }
        public string? ProfessionalName { get; set; }

        public Guid CompanyId { get; set; }

        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }

        public Guid AppointmentId { get; set; }

        public string Address { get; set; } = string.Empty;

        public Guid? TeamId { get; set; }
        public string? TeamName { get; set; }

        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }

        public string Status { get; set; } = "pending";

        public string ServiceType { get; set; } = string.Empty;

        public string? Notes { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
