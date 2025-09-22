using GeoSense.API.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace GeoSense.API.Examples
{
    public class UsuarioDTOExample : IExamplesProvider<UsuarioDTO>
    {
        public UsuarioDTO GetExamples()
        {
            return new UsuarioDTO
            {
                Id = 1,
                Nome = "Rafael de Souza Pinto",
                Email = "rafael.pinto@exemplo.com",
                Senha = "12345678",
                Tipo = 0 // ADMINISTRADOR
            };
        }
    }
}