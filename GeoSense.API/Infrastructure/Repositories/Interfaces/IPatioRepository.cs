using GeoSense.API.Infrastructure.Persistence;

namespace GeoSense.API.Infrastructure.Repositories.Interfaces
{
    public interface IPatioRepository
    {
        Task<List<Patio>> ObterTodasAsync();
        Task<Patio?> ObterPorIdAsync(long id);
        Task<Patio> AdicionarAsync(Patio patio);
        Task AtualizarAsync(Patio patio);
        Task RemoverAsync(Patio patio);
    }
}