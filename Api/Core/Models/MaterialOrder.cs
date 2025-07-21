using System;
using Core.Enums.Material;

namespace Core.Models
{
    public class MaterialOrder : BaseModel
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public int RequestedQuantity { get; set; }
        public int? ApprovedQuantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalValue { get; set; }
        public MaterialOrderStatus Status { get; set; }
        public MaterialOrderPriority Priority { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedByName { get; set; }
        public string? ApprovedBy { get; set; }
        public string? ApprovedByName { get; set; }
        public string? Supplier { get; set; }
        public DateTime? ExpectedDelivery { get; set; }
        public DateTime? ActualDelivery { get; set; }
        public string? Notes { get; set; }
        public int CompanyId { get; set; }

        // Navegação
        public Material? Material { get; set; }
        public Company? Company { get; set; }
    }
}
