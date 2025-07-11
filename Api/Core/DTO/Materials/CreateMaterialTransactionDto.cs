using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Materials
{
    public class CreateMaterialTransactionDto
    {
        [Required]
        public int MaterialId { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        [Required]
        public string Reason { get; set; }

        public int? ProfessionalId { get; set; }
        public int? AppointmentId { get; set; }
        public string? Notes { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}
