namespace GeoSense.API.DTOs
{
    /// <summary>
    /// Representa os dados de listagem de uma Moto.
    /// </summary>
    public class MotoListagemDTO
    {
        /// <summary>
        /// Identificador único da moto.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Modelo da moto.
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// Status da vaga associada.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Tipo (cliente) da vaga associada.
        /// </summary>
        public string Cliente { get; set; }
    }
}