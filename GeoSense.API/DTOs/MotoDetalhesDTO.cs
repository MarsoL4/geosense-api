namespace GeoSense.API.DTOs
{
    /// <summary>
    /// Representa os dados de detalhe de uma Moto.
    /// </summary>
    public class MotoDetalhesDTO
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
        /// Placa da moto.
        /// </summary>
        public string Placa { get; set; }

        /// <summary>
        /// Chassi da moto.
        /// </summary>
        public string Chassi { get; set; }

        /// <summary>
        /// Problema identificado na moto.
        /// </summary>
        public string ProblemaIdentificado { get; set; }

        /// <summary>
        /// Id da vaga associada.
        /// </summary>
        public long VagaId { get; set; }
    }
}