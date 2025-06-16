using Core.Enums;
using System;

namespace Core.DTO.Appointment
{
    public class CreateAppointmentDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Guid CompanyId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? TeamId { get; set; }
        public Guid? ProfessionalId { get; set; }

        public AppointmentStatus? Status { get; set; }
        public AppointmentType? Type { get; set; }

        public string? Notes { get; set; }
    }
}
