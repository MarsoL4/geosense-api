namespace GeoSense.API.Infrastructure.Persistence
{
    public class AlocacaoMoto
    {
        public long Id { get; set; }
        public Guid MotoId { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime? Saida { get; set; }
        public Moto Moto { get; set; }
    }

}
