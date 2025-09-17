using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoSense.API.Infrastructure.Contexts;
using GeoSense.API.Infrastructure.Persistence;
using GeoSense.API.DTOs;

namespace GeoSense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly GeoSenseContext _context;

        public MotoController(GeoSenseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDTO<Moto>>> GetMotos([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Motos.Include(m => m.Vaga).AsQueryable();
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResultDTO<Moto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Moto>> GetMoto(long id)
        {
            var moto = await _context.Motos
                .Include(m => m.Vaga)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (moto == null)
            {
                return NotFound();
            }

            return moto;
        }

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

        [HttpPost]
        public async Task<ActionResult<Moto>> PostMoto(MotoDTO dto)
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

            return CreatedAtAction(nameof(GetMoto), new { id = novaMoto.Id }, novaMoto);
        }

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