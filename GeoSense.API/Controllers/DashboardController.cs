using GeoSense.API.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using GeoSense.API.DTOs;

namespace GeoSense.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DashboardController(DashboardService service) : ControllerBase
    {
        private readonly DashboardService _service = service;

        /// <summary>
        /// Retorna dados agregados para o dashboard: totais de motos, vagas, problemas e tempo médio de permanência usando ML.NET.
        /// </summary>
        /// <remarks>
        /// Inclui contagem de total de motos, motos com problema, vagas livres, vagas ocupadas, total de vagas e uma previsão do tempo médio de permanência das motos (em horas), via ML.NET.
        /// </remarks>
        /// <response code="200">Dados agregados do dashboard, incluindo tempo médio de permanência previsto por ML.NET.</response>
        [HttpGet]
        [SwaggerResponse(200, "Dados agregados para o dashboard", typeof(DashboardDTO))]
        public async Task<IActionResult> GetDashboardData()
        {
            var resultado = await _service.ObterDashboardDataAsync();
            return Ok(resultado);
        }
    }
}