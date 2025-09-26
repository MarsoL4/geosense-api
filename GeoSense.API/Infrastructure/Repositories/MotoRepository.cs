using GeoSense.API.Infrastructure.Contexts;
using GeoSense.API.Infrastructure.Persistence;
using GeoSense.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoSense.API.Infrastructure.Repositories
{
    public class MotoRepository(GeoSenseContext context) : IMotoRepository
    {
        private readonly GeoSenseContext _context = context;

        public async Task<List<Moto>> ObterTodasAsync()
        {
            return await _context.Motos
                .Include(m => m.Vaga)
                .ToListAsync();
        }

        public async Task<Moto?> ObterPorIdComVagaEDefeitosAsync(long id)
        {
            return await _context.Motos
                .Include(m => m.Vaga)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}