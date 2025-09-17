using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoSense.API.Infrastructure.Contexts;
using GeoSense.API.Infrastructure.Persistence;
using GeoSense.API.DTOs;
using GeoSense.API.Helpers;
using Swashbuckle.AspNetCore.Filters;

namespace GeoSense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatioController : ControllerBase
    {
        private readonly GeoSenseContext _context;

        public PatioController(GeoSenseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna uma lista paginada de pátios cadastrados.
        /// </summary>
        /// <param name="page">Número da página (padrão: 1)</param>
        /// <param name="pageSize">Quantidade de itens por página (padrão: 10)</param>
        /// <returns>Página de pátios</returns>
        [HttpGet]
        public async Task<ActionResult<PagedHateoasDTO<Patio>>> GetPatios([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Patios.AsQueryable();
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var links = HateoasHelper.GetPagedLinks(Url, "Patios", page, pageSize, totalCount);

            var result = new PagedHateoasDTO<Patio>
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
        /// Retorna os dados de um pátio por ID.
        /// </summary>
        /// <param name="id">ID do pátio</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Patio>> GetPatio(long id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();
            return patio;
        }

        /// <summary>
        /// Cadastra um novo pátio.
        /// </summary>
        [HttpPost]
        [SwaggerRequestExample(typeof(PatioDTO), typeof(GeoSense.API.Examples.PatioDTOExample))]
        public async Task<ActionResult<Patio>> PostPatio(PatioDTO dto)
        {
            var novoPatio = new Patio();
            _context.Patios.Add(novoPatio);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPatio), new { id = novoPatio.Id }, novoPatio);
        }

        /// <summary>
        /// Atualiza os dados de um pátio existente.
        /// </summary>
        /// <param name="id">ID do pátio</param>
        /// <param name="dto">Dados para atualização</param>
        [HttpPut("{id}")]
        [SwaggerRequestExample(typeof(PatioDTO), typeof(GeoSense.API.Examples.PatioDTOExample))]
        public async Task<IActionResult> PutPatio(long id, PatioDTO dto)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Exclui um pátio do sistema.
        /// </summary>
        /// <param name="id">ID do pátio</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatio(long id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();

            _context.Patios.Remove(patio);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}