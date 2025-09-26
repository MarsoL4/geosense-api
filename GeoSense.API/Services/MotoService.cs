using GeoSense.API.DTOs.Moto;
using GeoSense.API.Infrastructure.Persistence;
using GeoSense.API.Infrastructure.Repositories.Interfaces;

namespace GeoSense.API.Services
{
    public class MotoService(IMotoRepository repo)
    {
        private readonly IMotoRepository _repo = repo;

        public async Task<List<Moto>> ObterTodasAsync()
        {
            return await _repo.ObterTodasAsync();
        }

        public async Task<Moto?> ObterPorIdAsync(long id)
        {
            return await _repo.ObterPorIdComVagaEDefeitosAsync(id);
        }

        public async Task<Moto> AdicionarAsync(Moto moto)
        {
            return await _repo.AdicionarAsync(moto);
        }

        public async Task AtualizarAsync(Moto moto)
        {
            await _repo.AtualizarAsync(moto);
        }

        public async Task RemoverAsync(Moto moto)
        {
            await _repo.RemoverAsync(moto);
        }
    }
}