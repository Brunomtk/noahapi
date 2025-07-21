// Core/DTO/InternalReports/CreateInternalReportDto.cs
using System;

namespace Core.DTO.InternalReports
{
    public class CreateInternalReportDto
    {
        /// <summary>
        /// Título do relatório interno
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Id do profissional que gerou o relatório
        /// </summary>
        public int ProfessionalId { get; set; }

        /// <summary>
        /// Id da equipe associada ao relatório
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// Categoria do relatório
        /// </summary>
        public string Category { get; set; } = null!;

        /// <summary>
        /// Status do relatório ("Pending", "InProgress", "Resolved")
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Data do relatório (se não informado, usa DateTime.UtcNow)
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Descrição detalhada do relatório
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// Prioridade do relatório ("Low", "Medium", "High")
        /// </summary>
        public string? Priority { get; set; }

        /// <summary>
        /// Id do usuário ao qual o relatório está atribuído
        /// </summary>
        public int AssignedToId { get; set; }
    }
}
