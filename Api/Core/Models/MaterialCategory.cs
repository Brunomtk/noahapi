using System;

namespace Core.Models
{
    public class MaterialCategory : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public int CompanyId { get; set; }

        // Navegação
        public Company? Company { get; set; }
    }
}
