namespace GeoSense.API.DTOs
{
    /// <summary>
    /// Dados agregados para o dashboard da aplicação.
    /// </summary>
    public class DashboardDTO
    {
        /// <summary>
        /// Total de motos no pátio.
        /// </summary>
        public int TotalMotos { get; set; }

        /// <summary>
        /// Número de motos com problema identificado.
        /// </summary>
        public int MotosComProblema { get; set; }

        /// <summary>
        /// Número de vagas livres.
        /// </summary>
        public int VagasLivres { get; set; }

        /// <summary>
        /// Número de vagas ocupadas.
        /// </summary>
        public int VagasOcupadas { get; set; }

        /// <summary>
        /// Total de vagas no pátio.
        /// </summary>
        public int TotalVagas { get; set; }

        /// <summary>
        /// Tempo médio de permanência das motos, em horas. (Previsão baseada em ML.NET)
        /// </summary>
        public double TempoMedioPermanenciaHoras { get; set; }
    }
}