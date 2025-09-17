using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoSense.API.Infrastructure.Contexts;
using GeoSense.API.Infrastructure.Persistence;
using GeoSense.API.DTOs;
using GeoSense.API.Domain.Enums;
using GeoSense.API.Helpers;
using AutoMapper;

namespace GeoSense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VagaController : ControllerBase
    {
        private readonly GeoSenseContext _context;
        private readonly IMapper _mapper;

        public VagaController(GeoSenseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna uma lista paginada de vagas cadastradas.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedHateoasDTO<VagaDTO>>> GetVagas([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Vagas.AsQueryable();
            var totalCount = await query.CountAsync();
            var vagas = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = _mapper.Map<List<VagaDTO>>(vagas);

            var links = HateoasHelper.GetPagedLinks(Url, "Vagas", page, pageSize, totalCount);

            var result = new PagedHateoasDTO<VagaDTO>
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
        [HttpGet("{id}")]
        public async Task<ActionResult<VagaDTO>> GetVaga(long id)
        {
            var vaga = await _context.Vagas.FindAsync(id);

            if (vaga == null)
                return NotFound();

            var dto = _mapper.Map<VagaDTO>(vaga);
            return Ok(dto);
        }

        /// <summary>
        /// Cadastra uma nova vaga.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<VagaDTO>> PostVaga(VagaDTO dto)
        {
            var novaVaga = new Vaga(dto.Numero, dto.PatioId);

            novaVaga.GetType().GetProperty("Tipo")?.SetValue(novaVaga, (TipoVaga)dto.Tipo);
            novaVaga.GetType().GetProperty("Status")?.SetValue(novaVaga, (StatusVaga)dto.Status);

            _context.Vagas.Add(novaVaga);
            await _context.SaveChangesAsync();

            var vagaCompleta = await _context.Vagas.FindAsync(novaVaga.Id);
            var resultDto = _mapper.Map<VagaDTO>(vagaCompleta);

            return CreatedAtAction(nameof(GetVaga), new { id = novaVaga.Id }, resultDto);
        }

        /// <summary>
        /// Atualiza os dados de uma vaga existente.
        /// </summary>
        [HttpPut("{id}")]
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
    }
}