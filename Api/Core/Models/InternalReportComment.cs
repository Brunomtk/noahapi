// Core/Models/InternalReportComment.cs
using System;

namespace Core.Models
{
    public class InternalReportComment : BaseModel
    {
        // Chave estrangeira para o relatório
        public int InternalReportId { get; set; }
        public InternalReport InternalReport { get; set; } = null!;

        public string AuthorId { get; set; } = null!;
        public string Author { get; set; } = null!;

        public DateTime Date { get; set; }

        public string Text { get; set; } = null!;

        // Herança de BaseModel já traz CreatedDate e UpdatedDate
        // public DateTime CreatedDate { get; set; }
        // public DateTime UpdatedDate { get; set; }
    }
}
