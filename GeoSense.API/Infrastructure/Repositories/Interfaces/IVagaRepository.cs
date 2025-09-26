using GeoSense.API.Infrastructure.Persistence;

namespace GeoSense.API.Infrastructure.Repositories.Interfaces
{
    public interface IVagaRepository
    {
        Task<List<Vaga>> ObterTodasAsync();
        Task<Vaga?> ObterPorIdAsync(long id);
        Task<Vaga> AdicionarAsync(Vaga vaga);
        Task AtualizarAsync(Vaga vaga);
        Task RemoverAsync(Vaga vaga);
    }
}