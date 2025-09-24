using AutoMapper;
using GeoSense.API.DTOs;
using GeoSense.API.DTOs.Patio;
using GeoSense.API.DTOs.Vaga;
using GeoSense.API.Helpers;
using GeoSense.API.Infrastructure.Contexts;
using GeoSense.API.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace GeoSense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatioController(GeoSenseContext context, IMapper mapper) : ControllerBase
    {
        private readonly GeoSenseContext _context = context;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Retorna uma lista paginada de pátios cadastrados (apenas dados simples).
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "Lista paginada de pátios cadastrados", typeof(PagedHateoasDTO<PatioDTO>))]
        public async Task<ActionResult<PagedHateoasDTO<PatioDTO>>> GetPatios([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Patios.AsNoTracking();
            var totalCount = await query.CountAsync();
            var patios = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = patios.Select(x => new PatioDTO { Id = x.Id, Nome = x.Nome }).ToList();

            var links = HateoasHelper.GetPagedLinks(Url, "Patios", page, pageSize, totalCount);

            var result = new PagedHateoasDTO<PatioDTO>
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
        /// Retorna os dados de um pátio por ID, incluindo suas vagas.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Pátio encontrado", typeof(PatioDetalhesDTO))]
        [SwaggerResponse(404, "Pátio não encontrado")]
        public async Task<ActionResult<PatioDetalhesDTO>> GetPatio(long id)
        {
            var patio = await _context.Patios
                .Include(p => p.Vagas)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patio == null)
                return NotFound();

            var dto = new PatioDetalhesDTO
            {
                Id = patio.Id,
                Nome = patio.Nome,
                Vagas = [.. patio.Vagas.Select(v => new VagaDTO
                {
                    Numero = v.Numero,
                    Tipo = (int)v.Tipo,
                    Status = (int)v.Status,
                    PatioId = v.PatioId
                })]
            };

            return Ok(dto);
        }

        /// <summary>
        /// Cadastra um novo pátio.
        /// </summary>
        [HttpPost]
        [SwaggerRequestExample(typeof(PatioDTO), typeof(GeoSense.API.Examples.PatioDTOExample))]
        [SwaggerResponse(201, "Pátio criado com sucesso", typeof(PatioDTO))]
        public async Task<ActionResult<PatioDTO>> PostPatio(PatioDTO _dto)
        {
            var novoPatio = new Patio { Nome = _dto.Nome };
            _context.Patios.Add(novoPatio);
            await _context.SaveChangesAsync();

            var patioCompleto = await _context.Patios.FindAsync(novoPatio.Id);
            var resultDto = new PatioDTO { Id = patioCompleto!.Id, Nome = patioCompleto.Nome };

            return CreatedAtAction(nameof(GetPatio), new { id = novoPatio.Id }, resultDto);
        }

        /// <summary>
        /// Atualiza os dados de um pátio existente.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerRequestExample(typeof(PatioDTO), typeof(GeoSense.API.Examples.PatioDTOExample))]
        [SwaggerResponse(204, "Pátio atualizado com sucesso")]
        [SwaggerResponse(404, "Pátio não encontrado")]
        public async Task<IActionResult> PutPatio(long id, PatioDTO _dto)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();

            patio.Nome = _dto.Nome;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Exclui um pátio do sistema.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Pátio removido com sucesso")]
        [SwaggerResponse(404, "Pátio não encontrado")]
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