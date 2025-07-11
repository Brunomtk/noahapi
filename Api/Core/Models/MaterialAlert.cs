using System;
using Core.Enums;

namespace Core.Models
{
    public class MaterialAlert : BaseModel
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public MaterialAlertType Type { get; set; }
        public string Message { get; set; }
        public AlertSeverity Severity { get; set; }
        public bool IsRead { get; set; }
        public int CompanyId { get; set; }

        // Navegação
        public Material? Material { get; set; }
        public Company? Company { get; set; }
    }
}
