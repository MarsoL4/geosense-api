using GeoSense.API.DTOs;
using GeoSense.API.Infrastructure.Repositories.Interfaces;

namespace GeoSense.API.Services
{
    /// <summary>
    /// Serviço responsável por retornar dados agregados para o dashboard.
    /// </summary>
    public class DashboardService(IMotoRepository motoRepo, IVagaRepository vagaRepo, MlPredictionService mlService)
    {
        private readonly IMotoRepository _motoRepo = motoRepo;
        private readonly IVagaRepository _vagaRepo = vagaRepo;
        private readonly MlPredictionService _mlService = mlService;

        /// <summary>
        /// Retorna dados agregados para o dashboard: totais de motos, vagas, problemas e tempo médio de permanência (via ML.NET).
        /// </summary>
        public async Task<DashboardDTO> ObterDashboardDataAsync()
        {
            var motos = await _motoRepo.ObterTodasAsync();
            var vagas = await _vagaRepo.ObterTodasAsync();

            var totalMotos = motos.Count;
            var motosComProblema = motos.Count(m => !string.IsNullOrEmpty(m.ProblemaIdentificado));
            var vagasOcupadas = vagas.Count(v => v.Motos.Count > 0);
            var vagasLivres = vagas.Count(v => v.Motos.Count == 0);
            var totalVagas = vagas.Count;

            // Chama a regressão do ML.NET
            var tempoMedioHoras = (double)_mlService.PreverTempoMedio(totalMotos, vagasLivres, motosComProblema);

            return new DashboardDTO
            {
                TotalMotos = totalMotos,
                MotosComProblema = motosComProblema,
                VagasLivres = vagasLivres,
                VagasOcupadas = vagasOcupadas,
                TotalVagas = totalVagas,
                TempoMedioPermanenciaHoras = Math.Round(tempoMedioHoras, 2)
            };
        }
    }
}