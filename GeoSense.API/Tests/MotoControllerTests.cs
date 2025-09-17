using Xunit;
using GeoSense.API.Controllers;
using GeoSense.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using GeoSense.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GeoSense.API.Tests
{
    public class MotoControllerTests
    {
        [Fact]
        public async Task PostMoto_DeveRetornarCreated()
        {
            var options = new DbContextOptionsBuilder<GeoSenseContext>()
                .UseInMemoryDatabase(databaseName: "GeoSenseTestDb")
                .Options;

            using var context = new GeoSenseContext(options);
            var controller = new MotoController(context);

            var dto = new MotoDTO
            {
                Modelo = "Teste",
                Placa = "ABC1234",
                Chassi = "CHASSITESTE",
                ProblemaIdentificado = "Nenhum",
                VagaId = 1
            };

            var result = await controller.PostMoto(dto);

            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
    }
}