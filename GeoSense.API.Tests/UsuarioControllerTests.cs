using Xunit;
using GeoSense.API.Controllers;
using GeoSense.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using GeoSense.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using GeoSense.API.AutoMapper;
using System.Threading.Tasks;

namespace GeoSense.API.Tests
{
    public class UsuarioControllerTests
    {
        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            return config.CreateMapper();
        }

        [Fact]
        public async Task PostUsuario_DeveRetornarCreated()
        {
            var options = new DbContextOptionsBuilder<GeoSenseContext>()
                .UseInMemoryDatabase(databaseName: "GeoSenseTestDb_Usuario")
                .Options;

            using var context = new GeoSenseContext(options);
            var mapper = CreateMapper();
            var controller = new UsuarioController(context, mapper);

            var dto = new UsuarioDTO
            {
                Nome = "Teste",
                Email = "teste@exemplo.com",
                Senha = "senha123",
                Tipo = 0 // ADMINISTRADOR
            };

            var result = await controller.PostUsuario(dto);

            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task GetUsuario_DeveRetornarNotFound_SeNaoExistir()
        {
            var options = new DbContextOptionsBuilder<GeoSenseContext>()
                .UseInMemoryDatabase(databaseName: "GeoSenseTestDb_Usuario_NotFound")
                .Options;

            using var context = new GeoSenseContext(options);
            var mapper = CreateMapper();
            var controller = new UsuarioController(context, mapper);

            var result = await controller.GetUsuario(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}