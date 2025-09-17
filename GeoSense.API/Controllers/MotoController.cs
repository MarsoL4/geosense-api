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
    public class MotoController : ControllerBase
    {
        private readonly GeoSenseContext _context;
        private readonly IMapper _mapper;

        public MotoController(GeoSenseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna uma lista paginada de motos cadastradas.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedHateoasDTO<MotoDetalhesDTO>>> GetMotos([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Motos.Include(m => m.Vaga);
            var totalCount = await query.CountAsync();
            var motos = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = _mapper.Map<List<MotoDetalhesDTO>>(motos);

            var links = HateoasHelper.GetPagedLinks(Url, "Motos", page, pageSize, totalCount);

            var result = new PagedHateoasDTO<MotoDetalhesDTO>
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
        /// Retorna os dados de uma moto por ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<MotoDetalhesDTO>> GetMoto(long id)
        {
            var moto = await _context.Motos
                .FirstOrDefaultAsync(m => m.Id == id);

            if (moto == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<MotoDetalhesDTO>(moto);
            return Ok(dto);
        }

        /// <summary>
        /// Atualiza os dados de uma moto existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoto(long id, MotoDTO dto)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null)
                return NotFound();

            moto.Modelo = dto.Modelo;
            moto.Placa = dto.Placa;
            moto.Chassi = dto.Chassi;
            moto.ProblemaIdentificado = dto.ProblemaIdentificado;
            moto.VagaId = dto.VagaId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Cadastra uma nova moto.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<MotoDetalhesDTO>> PostMoto(MotoDTO dto)
        {
            var novaMoto = new Moto
            {
                Modelo = dto.Modelo,
                Placa = dto.Placa,
                Chassi = dto.Chassi,
                ProblemaIdentificado = dto.ProblemaIdentificado,
                VagaId = dto.VagaId
            };

            _context.Motos.Add(novaMoto);
            await _context.SaveChangesAsync();

            var motoCompleta = await _context.Motos
                .FirstOrDefaultAsync(m => m.Id == novaMoto.Id);

            var resultDto = _mapper.Map<MotoDetalhesDTO>(motoCompleta);

            return CreatedAtAction(nameof(GetMoto), new { id = novaMoto.Id }, resultDto);
        }

        /// <summary>
        /// Exclui uma moto do sistema.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoto(long id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null)
                return NotFound();

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MotoExists(long id)
        {
            return _context.Motos.Any(e => e.Id == id);
        }
    }
}