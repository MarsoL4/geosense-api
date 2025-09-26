using GeoSense.API.Infrastructure.Persistence;

namespace GeoSense.API.Infrastructure.Repositories.Interfaces
{
    public interface IMotoRepository
    {
        Task<List<Moto>> ObterTodasAsync();
        Task<Moto?> ObterPorIdComVagaEDefeitosAsync(long id);
    }
}