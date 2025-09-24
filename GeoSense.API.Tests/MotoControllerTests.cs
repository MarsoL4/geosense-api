using Xunit;
using GeoSense.API.Controllers;
using GeoSense.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using GeoSense.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using GeoSense.API.AutoMapper;

namespace GeoSense.API.Tests
{
    public class MotoControllerTests
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
        public async Task PostMoto_DeveRetornarCreated()
        {
            var options = new DbContextOptionsBuilder<GeoSenseContext>()
                .UseInMemoryDatabase(databaseName: "GeoSenseTestDb")
                .Options;

            using var context = new GeoSenseContext(options);
            context.Vagas.Add(new GeoSense.API.Infrastructure.Persistence.Vaga(1, 1));
            await context.SaveChangesAsync();

            var mapper = CreateMapper();
            var controller = new MotoController(context, mapper);

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