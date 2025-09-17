namespace GeoSense.API.DTOs
{
    /// <summary>
    /// Representa os dados necessários para cadastrar ou atualizar uma Moto.
    /// </summary>
    public class MotoDTO
    {
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