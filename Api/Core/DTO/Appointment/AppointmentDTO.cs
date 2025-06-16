using System;

namespace Core.DTO.Appointment
{
    public class AppointmentDTO
    {
        public Guid Id { get; set; }  // Corrigido de int para Guid

        public string Title { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string Status { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? Notes { get; set; }

        public Guid CompanyId { get; set; }
        public string? CompanyName { get; set; }

        public Guid? CustomerId { get; set; }
        public string? CustomerName { get; set; }

        public Guid? TeamId { get; set; }
        public string? TeamName { get; set; }

        public Guid? ProfessionalId { get; set; }
        public string? ProfessionalName { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
