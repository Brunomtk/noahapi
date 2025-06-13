using Core.Enums;

namespace Core.DTO.Company
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string Responsible { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? PlanName { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
