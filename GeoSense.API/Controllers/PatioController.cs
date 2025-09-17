using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoSense.API.Infrastructure.Contexts;
using GeoSense.API.Infrastructure.Persistence;
using GeoSense.API.DTOs;

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

        [HttpGet]
        public async Task<ActionResult<PagedResultDTO<Patio>>> GetPatios([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Patios.AsQueryable();
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResultDTO<Patio>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patio>> GetPatio(long id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();
            return patio;
        }

        [HttpPost]
        public async Task<ActionResult<Patio>> PostPatio()
        {
            var novoPatio = new Patio();
            _context.Patios.Add(novoPatio);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPatio), new { id = novoPatio.Id }, novoPatio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatio(long id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();

            await _context.SaveChangesAsync();
            return NoContent();
        }

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