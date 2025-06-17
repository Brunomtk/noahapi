using System;

namespace Core.DTO.CheckRecords
{
    public class UpdateCheckRecordDTO
    {
        public Guid? ProfessionalId { get; set; }
        public string? ProfessionalName { get; set; }

        public Guid? CompanyId { get; set; }

        public Guid? CustomerId { get; set; }
        public string? CustomerName { get; set; }

        public Guid? AppointmentId { get; set; }

        public string? Address { get; set; }

        public Guid? TeamId { get; set; }
        public string? TeamName { get; set; }

        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }

        public string? Status { get; set; }

        public string? ServiceType { get; set; }

        public string? Notes { get; set; }
    }
}
