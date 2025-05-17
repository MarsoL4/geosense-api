namespace GeoSense.API.Services
{
    using global::GeoSense.API.DTOs;
    using global::GeoSense.API.Infrastructure.Contexts;
    using Microsoft.EntityFrameworkCore;

    namespace GeoSense.Application.Services
    {
        public class DashboardService
        {
            private readonly GeoSenseContext _context;

            public DashboardService(GeoSenseContext context)
            {
                _context = context;
            }

            public async Task<DashboardDTO> ObterDashboardAsync()
            {
                var hoje = DateTime.Today;

                // Fix: Corrected property name to 'Entrada' as 'DataHoraAlocacao' does not exist in 'AlocacaoMoto'  
                var motosHoje = await _context.AlocacoesMoto
                    .Where(a => a.Entrada.Date == hoje)
                    .CountAsync();

                // Fix: Replaced 'EF.Functions.DateDiffMinute' with a manual calculation since 'DateDiffMinute' is not available  
                var tempos = await _context.AlocacoesMoto
                    .Select(a => (int)(DateTime.Now - a.Entrada).TotalMinutes)
                    .ToListAsync();

                double mediaHoras = tempos.Count > 0 ? tempos.Average() / 60.0 : 0;

                return new DashboardDTO
                {
                    TotalMotosHoje = motosHoje,
                    TempoMedioPermanenciaHoras = Math.Round(mediaHoras, 2)
                };
            }
        }
    }
}
