using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoSense.API.Infrastructure.Contexts;
using GeoSense.API.Infrastructure.Persistence;

namespace GeoSense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly MotoContext _context;

        public MotoController(MotoContext context)
        {
            _context = context;
        }

        // GET: api/Moto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moto>>> GetMoto()
        {
            return await _context.Moto.ToListAsync();
        }

        // GET: api/Moto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Moto>> GetMoto(Guid id)
        {
            var moto = await _context.Moto.FindAsync(id);

            if (moto == null)
            {
                return NotFound();
            }

            return moto;
        }

        // PUT: api/Moto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoto(Guid id, Moto moto)
        {
            if (id != moto.Id)
            {
                return BadRequest();
            }

            _context.Entry(moto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Moto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Moto>> PostMoto(Moto moto)
        {
            _context.Moto.Add(moto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoto", new { id = moto.Id }, moto);
        }

        // DELETE: api/Moto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoto(Guid id)
        {
            var moto = await _context.Moto.FindAsync(id);
            if (moto == null)
            {
                return NotFound();
            }

            _context.Moto.Remove(moto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MotoExists(Guid id)
        {
            return _context.Moto.Any(e => e.Id == id);
        }
    }
}
