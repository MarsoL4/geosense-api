namespace GeoSense.API.Infrastructure.Persistence
{
    public class Patio
    {
        public Guid Id { get; private set; }

        public ICollection<Vaga> Vagas { get; set; } = new List<Vaga>();

    }
}
