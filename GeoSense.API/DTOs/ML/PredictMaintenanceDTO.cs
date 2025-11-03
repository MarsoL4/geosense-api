namespace GeoSense.API.DTOs.ML
{
    /// <summary>
    /// Requisição para predizer necessidade de manutenção da moto.
    /// </summary>
    public class PredictMaintenanceDTO
    {
        public float TipoVaga { get; set; }
        public float StatusVaga { get; set; }
        public float TempoUsoHoras { get; set; }
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