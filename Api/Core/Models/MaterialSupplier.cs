using System;

namespace Core.Models
{
    public class MaterialSupplier : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public int CompanyId { get; set; }

        // Navegação
        public Company? Company { get; set; }
    }
}
