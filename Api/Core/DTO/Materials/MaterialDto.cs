using System;

namespace Core.DTO.Materials
{
    public class MaterialDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public int CurrentStock { get; set; }
        public int MinStock { get; set; }
        public int? MaxStock { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Supplier { get; set; }
        public string? SupplierContact { get; set; }
        public string? Barcode { get; set; }
        public string? Location { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Status { get; set; }
        public int CompanyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
