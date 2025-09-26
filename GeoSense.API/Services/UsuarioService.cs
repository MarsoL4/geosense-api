using GeoSense.API.Infrastructure.Persistence;
using GeoSense.API.Infrastructure.Repositories.Interfaces;

namespace GeoSense.API.Services
{
    public class UsuarioService(IUsuarioRepository repo)
    {
        private readonly IUsuarioRepository _repo = repo;

        public async Task<List<Usuario>> ObterTodasAsync() => await _repo.ObterTodasAsync();
        public async Task<Usuario?> ObterPorIdAsync(long id) => await _repo.ObterPorIdAsync(id);
        public async Task<Usuario> AdicionarAsync(Usuario usuario) => await _repo.AdicionarAsync(usuario);
        public async Task AtualizarAsync(Usuario usuario) => await _repo.AtualizarAsync(usuario);
        public async Task RemoverAsync(Usuario usuario) => await _repo.RemoverAsync(usuario);
        public async Task<bool> EmailExisteAsync(string email, long? ignoreId = null) => await _repo.EmailExisteAsync(email, ignoreId);
    }
}