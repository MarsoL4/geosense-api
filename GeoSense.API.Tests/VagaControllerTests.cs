using GeoSense.API.Controllers;
using GeoSense.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using GeoSense.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using GeoSense.API.Infrastructure.Persistence;
using GeoSense.API.Domain.Enums;

namespace GeoSense.API.Tests
{
    public class VagaControllerTests
    {
        [Fact]
        public async Task PostVaga_DeveRetornarCreated()
        {
            var options = new DbContextOptionsBuilder<GeoSenseContext>()
                .UseInMemoryDatabase(databaseName: "GeoSenseTestDb_Vaga")
                .Options;

            using var context = new GeoSenseContext(options);
            context.Patios.Add(new Patio { });
            await context.SaveChangesAsync();

            var controller = new VagaController(context);

            var dto = new VagaDTO
            {
                Numero = 1,
                Tipo = (int)TipoVaga.Reparo_Simples,
                Status = (int)StatusVaga.LIVRE,
                PatioId = context.Patios.FirstOrDefault().Id
            };

            var result = await controller.PostVaga(dto);

            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task GetVaga_DeveRetornarNotFound_SeNaoExistir()
        {
            var options = new DbContextOptionsBuilder<GeoSenseContext>()
                .UseInMemoryDatabase(databaseName: "GeoSenseTestDb_Vaga_NotFound")
                .Options;

            using var context = new GeoSenseContext(options);
            var controller = new VagaController(context);

            var result = await controller.GetVaga(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}