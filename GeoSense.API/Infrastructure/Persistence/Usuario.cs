using GeoSense.API.Domain.Enums;

namespace GeoSense.API.Infrastructure.Persistence
{
    public class Usuario(long id, string nome, string email, string senha, TipoUsuario tipo)
    {
        public long Id { get; private set; } = id;
        public string Nome { get; private set; } = nome;
        public string Email { get; private set; } = email;
        public string Senha { get; private set; } = senha;
        public TipoUsuario Tipo { get; private set; } = tipo;
    }
}