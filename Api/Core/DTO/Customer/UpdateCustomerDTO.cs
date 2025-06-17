﻿using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Customer
{
    public class UpdateCustomerDTO
    {
        [Required]
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Document { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Observations { get; set; }

        public StatusEnum? Status { get; set; }
    }
}
