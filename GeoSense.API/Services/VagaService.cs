using GeoSense.API.Domain.Enums;
using GeoSense.API.DTOs;
using GeoSense.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GeoSense.API.Services
{
    public class VagaService
    {
        private readonly GeoSenseContext _context;

        public VagaService(GeoSenseContext context)
        {
            _context = context;
        }

        public async Task<VagasStatusDTO> ObterVagasLivresAsync()
        {
            var livres = await _context.Vagas
                .Where(v => v.Status == StatusVaga.LIVRE)
                .ToListAsync();

            var comProblema = livres.Count(v => v.Tipo != TipoVaga.Sem_Problema);
            var semProblema = livres.Count(v => v.Tipo == TipoVaga.Sem_Problema);

            return new VagasStatusDTO
            {
                LivresComProblema = comProblema,
                LivresSemProblema = semProblema
            };
        }
    }
}