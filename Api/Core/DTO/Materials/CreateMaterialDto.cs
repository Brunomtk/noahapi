using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Materials
{
    public class CreateMaterialDto
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Unit { get; set; }

        [Required]
        public int CurrentStock { get; set; }

        [Required]
        public int MinStock { get; set; }

        public int? MaxStock { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public string? Supplier { get; set; }
        public string? SupplierContact { get; set; }
        public string? Barcode { get; set; }
        public string? Location { get; set; }
        public DateTime? ExpirationDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}
