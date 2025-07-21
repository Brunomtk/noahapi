using System;
using System.Collections.Generic;
using Core.Enums;

namespace Core.Models
{
    public class InternalReport : BaseModel
    {
        public string Title { get; set; } = null!;
        public int ProfessionalId { get; set; }
        public string Professional { get; set; } = null!;
        public int TeamId { get; set; }
        public string Team { get; set; } = null!;
        public string Category { get; set; } = null!;
        public InternalReportStatus Status { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
        public InternalReportPriority Priority { get; set; }
        public int AssignedToId { get; set; }
        public string AssignedTo { get; set; } = null!;

        public ICollection<InternalReportComment> Comments { get; set; } = new List<InternalReportComment>();
    }
}
