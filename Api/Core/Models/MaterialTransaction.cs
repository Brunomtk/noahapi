using System;
using Core.Enums;

namespace Core.Models
{
    public class MaterialTransaction : BaseModel
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public TransactionType Type { get; set; }
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalValue { get; set; }
        public string Reason { get; set; }
        public int? ProfessionalId { get; set; }
        public int? AppointmentId { get; set; }
        public string? Notes { get; set; }
        public DateTime Date { get; set; }
        public int CompanyId { get; set; }

        // Navegação
        public Material? Material { get; set; }
        public Professional? Professional { get; set; }
        public Appointment? Appointment { get; set; }
        public Company? Company { get; set; }
    }
}
