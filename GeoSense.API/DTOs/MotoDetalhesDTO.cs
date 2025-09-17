namespace GeoSense.API.DTOs
{
    /// <summary>
    /// Representa os detalhes completos de uma Moto, incluindo vaga e defeitos.
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
        /// Status da vaga associada.
        /// </summary>
        public string VagaStatus { get; set; }

        /// <summary>
        /// Tipo da vaga associada.
        /// </summary>
        public string VagaTipo { get; set; }

        /// <summary>
        /// Lista de defeitos associados à moto.
        /// </summary>
        public List<string> Defeitos { get; set; }
    }
}