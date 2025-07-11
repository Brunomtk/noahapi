namespace Core.DTO.Notifications
{
    /// <summary>
    /// Filtros para listagem de notificações.
    /// </summary>
    public class NotificationFiltersDTO
    {
        /// <summary>
        /// Filtrar pelo tipo de notificação ("Info", "Warning", "Error", "Success").
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Filtrar pelo papel do destinatário ("admin", "company", "professional").
        /// </summary>
        public string? RecipientRole { get; set; }

        /// <summary>
        /// Texto livre para busca em título ou mensagem.
        /// </summary>
        public string? Search { get; set; }
    }
}
