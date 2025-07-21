using System;

// Core/DTO/InternalReports/CreateInternalReportCommentDto.cs
namespace Core.DTO.InternalReports
{
    public class CreateInternalReportCommentDto
    {
        /// <summary>
        /// Id do relatório ao qual este comentário pertence
        /// </summary>
        public int InternalReportId { get; set; }

        /// <summary>
        /// Id do usuário autor do comentário
        /// </summary>
        public string AuthorId { get; set; } = null!;

        /// <summary>
        /// Texto do comentário
        /// </summary>
        public string Text { get; set; } = null!;
    }
}





