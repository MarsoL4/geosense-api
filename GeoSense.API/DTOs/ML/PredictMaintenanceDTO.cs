namespace GeoSense.API.DTOs.ML
{
    /// <summary>
    /// Requisição para predizer necessidade de manutenção da moto.
    /// </summary>
    public class PredictMaintenanceDTO
    {
        public int TipoVaga { get; set; }
        public int StatusVaga { get; set; }
        public int TempoUsoHoras { get; set; }
    }

    /// <summary>
    /// Resposta da predição (simples binário: precisa manutenção ou não)
    /// </summary>
    public class PredictMaintenanceResultDTO
    {
        public bool PrecisaManutencao { get; set; }
        public float Score { get; set; }
    }
}