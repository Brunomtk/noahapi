using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Materials
{
    public class UpdateMaterialOrderStatusDto
    {
        [Required]
        public string Status { get; set; }

        public int? ApprovedQuantity { get; set; }
    }
}
