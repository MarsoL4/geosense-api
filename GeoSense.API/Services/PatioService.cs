using GeoSense.API.Infrastructure.Persistence;
using GeoSense.API.Infrastructure.Repositories.Interfaces;

namespace GeoSense.API.Services
{
    public class PatioService(IPatioRepository repo)
    {
        private readonly IPatioRepository _repo = repo;

        public async Task<List<Patio>> ObterTodasAsync() => await _repo.ObterTodasAsync();
        public async Task<Patio?> ObterPorIdAsync(long id) => await _repo.ObterPorIdAsync(id);
        public async Task<Patio> AdicionarAsync(Patio patio) => await _repo.AdicionarAsync(patio);
        public async Task AtualizarAsync(Patio patio) => await _repo.AtualizarAsync(patio);
        public async Task RemoverAsync(Patio patio) => await _repo.RemoverAsync(patio);
    }
}