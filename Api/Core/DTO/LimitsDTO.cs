namespace Core.DTO
{
    /// <summary>
    /// Define os limites num�ricos associados a um plano.
    /// </summary>
    public class LimitsDTO
    {
        /// <summary>
        /// Quantidade m�xima de profissionais permitidos.
        /// </summary>
        public int? Professionals { get; set; }

        /// <summary>
        /// Quantidade m�xima de equipes permitidas.
        /// </summary>
        public int? Teams { get; set; }

        /// <summary>
        /// Quantidade m�xima de clientes permitidos.
        /// </summary>
        public int? Customers { get; set; }

        /// <summary>
        /// Quantidade m�xima de agendamentos permitidos.
        /// </summary>
        public int? Appointments { get; set; }
    }
}
