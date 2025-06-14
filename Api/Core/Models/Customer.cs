using System;
using Core.Enums;

namespace Core.Models
{
    public class Customer : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string? Observations { get; set; }

        public StatusEnum Status { get; set; } = StatusEnum.Active;

        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
