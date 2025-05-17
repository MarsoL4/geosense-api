namespace GeoSense.API.Infrastructure.Persistence
{
    public class Defeito
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public Guid MotoId { get; set; }
        public Moto Moto { get; set; }
    }
}
