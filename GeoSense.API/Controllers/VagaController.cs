using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoSense.API.Infrastructure.Contexts;
using GeoSense.API.Infrastructure.Persistence;
using GeoSense.API.DTOs;
using GeoSense.API.Domain.Enums;
using GeoSense.API.Helpers;
using Swashbuckle.AspNetCore.Filters;

namespace GeoSense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VagaController : ControllerBase
    {
        private readonly GeoSenseContext _context;

        public VagaController(GeoSenseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna uma lista paginada de vagas cadastradas.
        /// </summary>
        /// <param name="page">Número da página (padrão: 1)</param>
        /// <param name="pageSize">Quantidade de itens por página (padrão: 10)</param>
        /// <returns>Página de vagas</returns>
        [HttpGet]
        public async Task<ActionResult<PagedHateoasDTO<Vaga>>> GetVagas([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Vagas.AsQueryable();
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var links = HateoasHelper.GetPagedLinks(Url, "Vagas", page, pageSize, totalCount);

            var result = new PagedHateoasDTO<Vaga>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Links = links
            };

            return Ok(result);
        }

        /// <summary>
        /// Retorna os dados de uma vaga por ID.
        /// </summary>
        /// <param name="id">ID da vaga</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Vaga>> GetVaga(long id)
        {
            var vaga = await _context.Vagas.FindAsync(id);

            if (vaga == null)
                return NotFound();

            return vaga;
        }

        /// <summary>
        /// Cadastra uma nova vaga.
        /// </summary>
        /// <param name="dto">Dados da nova vaga</param>
        [HttpPost]
        [SwaggerRequestExample(typeof(VagaDTO), typeof(GeoSense.API.Examples.VagaDTOExample))]
        public async Task<ActionResult<Vaga>> PostVaga(VagaDTO dto)
        {
            var novaVaga = new Vaga(dto.Numero, dto.PatioId);

            novaVaga.GetType().GetProperty("Tipo")?.SetValue(novaVaga, (TipoVaga)dto.Tipo);
            novaVaga.GetType().GetProperty("Status")?.SetValue(novaVaga, (StatusVaga)dto.Status);

            _context.Vagas.Add(novaVaga);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVaga), new { id = novaVaga.Id }, novaVaga);
        }

        /// <summary>
        /// Atualiza os dados de uma vaga existente.
        /// </summary>
        /// <param name="id">ID da vaga</param>
        /// <param name="dto">Dados para atualização</param>
        [HttpPut("{id}")]
        [SwaggerRequestExample(typeof(VagaDTO), typeof(GeoSense.API.Examples.VagaDTOExample))]
        public async Task<IActionResult> PutVaga(long id, VagaDTO dto)
        {
            var vaga = await _context.Vagas.FindAsync(id);
            if (vaga == null)
                return NotFound();

            vaga.GetType().GetProperty("Numero")?.SetValue(vaga, dto.Numero);
            vaga.GetType().GetProperty("Tipo")?.SetValue(vaga, (TipoVaga)dto.Tipo);
            vaga.GetType().GetProperty("Status")?.SetValue(vaga, (StatusVaga)dto.Status);
            vaga.GetType().GetProperty("PatioId")?.SetValue(vaga, dto.PatioId);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Exclui uma vaga do sistema.
        /// </summary>
        /// <param name="id">ID da vaga</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaga(long id)
        {
            var vaga = await _context.Vagas.FindAsync(id);
            if (vaga == null)
                return NotFound();

            _context.Vagas.Remove(vaga);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool VagaExists(long id)
        {
            return _context.Vagas.Any(v => v.Id == id);
        }
    }
}