using Xunit;
using GeoSense.API.Controllers;
using GeoSense.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using GeoSense.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using GeoSense.API.Infrastructure.Persistence;
using GeoSense.API.Domain.Enums;
using AutoMapper;
using GeoSense.API.AutoMapper;

namespace GeoSense.API.Tests
{
    public class VagaControllerTests
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
        public async Task PostVaga_DeveRetornarCreated()
        {
            var options = new DbContextOptionsBuilder<GeoSenseContext>()
                .UseInMemoryDatabase(databaseName: "GeoSenseTestDb_Vaga")
                .Options;

            using var context = new GeoSenseContext(options);
            context.Patios.Add(new Patio { Nome = "Pátio Central" });
            await context.SaveChangesAsync();

            var mapper = CreateMapper();
            var controller = new VagaController(context, mapper);

            var patio = context.Patios.FirstOrDefault();
            Assert.NotNull(patio);

            var dto = new VagaDTO
            {
                Numero = 1,
                Tipo = (int)TipoVaga.Reparo_Simples,
                Status = (int)StatusVaga.LIVRE,
                PatioId = patio.Id
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
            var mapper = CreateMapper();
            var controller = new VagaController(context, mapper);

            var result = await controller.GetVaga(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}