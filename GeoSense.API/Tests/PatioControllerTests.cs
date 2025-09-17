using Xunit;
using GeoSense.API.Controllers;
using GeoSense.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GeoSense.API.DTOs;
using GeoSense.API.Infrastructure.Persistence;
using System.Threading.Tasks;

namespace GeoSense.API.Tests
{
    public class PatioControllerTests
    {
        [Fact]
        public async Task PostPatio_DeveRetornarCreated()
        {
            var options = new DbContextOptionsBuilder<GeoSenseContext>()
                .UseInMemoryDatabase(databaseName: "GeoSenseTestDb_Patio")
                .Options;

            using var context = new GeoSenseContext(options);
            var controller = new PatioController(context);

            var dto = new PatioDTO { Id = 0 };

            var result = await controller.PostPatio(dto);

            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task GetPatio_DeveRetornarNotFound_SeNaoExistir()
        {
            var options = new DbContextOptionsBuilder<GeoSenseContext>()
                .UseInMemoryDatabase(databaseName: "GeoSenseTestDb_Patio_NotFound")
                .Options;

            using var context = new GeoSenseContext(options);
            var controller = new PatioController(context);

            var result = await controller.GetPatio(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}