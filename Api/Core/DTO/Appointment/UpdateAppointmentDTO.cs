using Core.Enums;
using System;

namespace Core.DTO.Appointment
{
    public class UpdateAppointmentDTO
    {
        public string? Title { get; set; }
        public string? Address { get; set; }

        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public Guid? CompanyId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? TeamId { get; set; }
        public Guid? ProfessionalId { get; set; }

        public AppointmentStatus? Status { get; set; }
        public AppointmentType? Type { get; set; }

        public string? Notes { get; set; }
    }
}
