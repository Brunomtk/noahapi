using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class CreateCustomerDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Document { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string? Observations { get; set; }

        [Required]
        public int CompanyId { get; set; } // ✅ Corrigido de Guid para int
    }
}
