using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Materials
{
    public class CreateMaterialOrderDto
    {
        [Required]
        public int MaterialId { get; set; }

        [Required]
        public int RequestedQuantity { get; set; }

        public decimal? UnitPrice { get; set; }

        [Required]
        public string Priority { get; set; }

        public string? Supplier { get; set; }
        public DateTime? ExpectedDelivery { get; set; }
        public string? Notes { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}
