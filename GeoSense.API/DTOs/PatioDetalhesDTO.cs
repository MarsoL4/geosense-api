namespace GeoSense.API.DTOs
{
    /// <summary>
    /// DTO de detalhes do pátio, incluindo vagas.
    /// </summary>
    public class PatioDetalhesDTO
    {
        public long Id { get; set; }
        public required string Nome { get; set; }
        public List<VagaDTO> Vagas { get; set; } = [];
    }
}