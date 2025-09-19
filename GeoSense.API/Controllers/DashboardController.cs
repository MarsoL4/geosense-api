﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoSense.API.Infrastructure.Contexts;
using GeoSense.API.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace GeoSense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly GeoSenseContext _context;

        public DashboardController(GeoSenseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna dados agregados para o dashboard: totais de motos, vagas e problemas.
        /// </summary>
        /// <remarks>
        /// Retorna informações resumidas sobre o sistema, incluindo total de motos, motos com problema, vagas livres e ocupadas.
        /// </remarks>
        /// <response code="200">Dados agregados para o dashboard</response>
        [HttpGet]
        [SwaggerResponse(200, "Dados agregados para o dashboard")]
        public async Task<IActionResult> GetDashboardData()
        {
            var totalMotos = await _context.Motos.CountAsync();
            var motosComProblema = await _context.Motos
                .CountAsync(m => !string.IsNullOrEmpty(m.ProblemaIdentificado));

            var vagasLivres = await _context.Vagas
                .CountAsync(v => v.Status == StatusVaga.LIVRE);

            var vagasOcupadas = await _context.Vagas
                .CountAsync(v => v.Status == StatusVaga.OCUPADA);

            var totalVagas = vagasLivres + vagasOcupadas;

            var resultado = new
            {
                TotalMotos = totalMotos,
                MotosComProblema = motosComProblema,
                VagasLivres = vagasLivres,
                VagasOcupadas = vagasOcupadas,
                TotalVagas = totalVagas
            };

            return Ok(resultado);
        }
    }
}