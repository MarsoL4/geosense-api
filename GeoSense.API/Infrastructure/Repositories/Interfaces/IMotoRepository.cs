using GeoSense.API.Infrastructure.Persistence;

namespace GeoSense.API.Infrastructure.Repositories.Interfaces
{
    public interface IMotoRepository
    {
        Task<List<Moto>> ObterTodasAsync();
        Task<Moto?> ObterPorIdComVagaEDefeitosAsync(long id);
        Task<Moto> AdicionarAsync(Moto moto);
        Task AtualizarAsync(Moto moto);
        Task RemoverAsync(Moto moto);
    }
}