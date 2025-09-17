using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoSense.API.Infrastructure.Contexts;
using GeoSense.API.Infrastructure.Persistence;
using GeoSense.API.DTOs;
using GeoSense.API.Helpers;
using AutoMapper;

namespace GeoSense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatioController : ControllerBase
    {
        private readonly GeoSenseContext _context;
        private readonly IMapper _mapper;

        public PatioController(GeoSenseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna uma lista paginada de pátios cadastrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedHateoasDTO<PatioDTO>>> GetPatios([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Patios.AsQueryable();
            var totalCount = await query.CountAsync();
            var patios = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = _mapper.Map<List<PatioDTO>>(patios);

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
        /// Retorna os dados de um pátio por ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PatioDTO>> GetPatio(long id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();

            var dto = _mapper.Map<PatioDTO>(patio);
            return Ok(dto);
        }

        /// <summary>
        /// Cadastra um novo pátio.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<PatioDTO>> PostPatio(PatioDTO dto)
        {
            var novoPatio = new Patio();
            _context.Patios.Add(novoPatio);
            await _context.SaveChangesAsync();

            var patioCompleto = await _context.Patios.FindAsync(novoPatio.Id);
            var resultDto = _mapper.Map<PatioDTO>(patioCompleto);

            return CreatedAtAction(nameof(GetPatio), new { id = novoPatio.Id }, resultDto);
        }

        /// <summary>
        /// Atualiza os dados de um pátio existente.
        /// </summary>
        [HttpPut("{id}")]
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