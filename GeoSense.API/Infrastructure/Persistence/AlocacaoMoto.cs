namespace GeoSense.API.Infrastructure.Persistence
{
    public class AlocacaoMoto
    {
        public Guid Id { get; set; }
        public DateTime DataHoraAlocacao { get; set; }
        public Guid MotoId { get; set; }
        public Guid VagaId { get; set; }
        public Guid MecanicoResponsavelId { get; set; }
        public Moto Moto { get; set; }
        public Vaga Vaga { get; set; }
    }

}
