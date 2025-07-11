using System;
using System.Collections.Generic;
using Core.Enums;

namespace Core.Models
{
    public class Material : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }       // ex: kg, liters, units
        public int CurrentStock { get; set; }
        public int MinStock { get; set; }
        public int? MaxStock { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Supplier { get; set; }
        public string? SupplierContact { get; set; }
        public string? Barcode { get; set; }
        public string? Location { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public MaterialStatus Status { get; set; }
        public int CompanyId { get; set; }

        // Navegação
        public Company? Company { get; set; }
        public ICollection<MaterialTransaction>? Transactions { get; set; }
        public ICollection<MaterialOrder>? Orders { get; set; }
    }
}
