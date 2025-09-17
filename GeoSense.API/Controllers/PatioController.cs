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

        // GET: api/patio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patio>>> GetPatios()
        {
            return await _context.Patios.ToListAsync();
        }

        // GET: api/patio/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Patio>> GetPatio(long id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();
            return patio;
        }

        // POST: api/patio
        [HttpPost]
        public async Task<ActionResult<Patio>> PostPatio(PatioDTO dto)
        {
            var novoPatio = new Patio();
            _context.Patios.Add(novoPatio);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPatio), new { id = novoPatio.Id }, novoPatio);
        }

        // PUT: api/patio/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatio(long id, PatioDTO dto)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/patio/{id}
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