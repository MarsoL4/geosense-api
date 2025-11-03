using Microsoft.ML;
using Microsoft.ML.Data;
using GeoSense.API.DTOs.ML;

namespace GeoSense.API.Services
{
    /// <summary>
    /// Serviço de predição usando ML.NET (simples demonstração).
    /// </summary>
    public class MlPredictionService
    {
        private readonly MLContext _mlContext;
        private readonly PredictionEngine<PredictMaintenanceDTO, MaintenancePrediction> _predictionEngine;

        public MlPredictionService()
        {
            _mlContext = new MLContext();

            // EXEMPLO BÁSICO com pipeline fixa e threshold simples
            var pipeline = _mlContext.Transforms.Concatenate("Features", nameof(PredictMaintenanceDTO.TipoVaga), nameof(PredictMaintenanceDTO.StatusVaga), nameof(PredictMaintenanceDTO.TempoUsoHoras))
                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", maximumNumberOfIterations: 10));

            // Treinamento fictício - dados fake (TREINO AD HOC SÓ P/ DEMO)
            var examples = new List<ModelInput>
            {
                new() { TipoVaga = 1, StatusVaga = 1, TempoUsoHoras = 10, Label = true },
                new() { TipoVaga = 0, StatusVaga = 0, TempoUsoHoras = 1, Label = false }
            };

            var trainData = _mlContext.Data.LoadFromEnumerable(examples);
            var model = pipeline.Fit(trainData);

            _predictionEngine = _mlContext.Model.CreatePredictionEngine<PredictMaintenanceDTO, MaintenancePrediction>(model);
        }

        public PredictMaintenanceResultDTO Predict(PredictMaintenanceDTO input)
        {
            var result = _predictionEngine.Predict(input);
            return new PredictMaintenanceResultDTO
            {
                PrecisaManutencao = result.PredictedLabel,
                Score = result.Probability
            };
        }

        private class ModelInput : PredictMaintenanceDTO
        {
            public bool Label { get; set; }
        }

        private class MaintenancePrediction : PredictMaintenanceResultDTO
        {
            [ColumnName("PredictedLabel")]
            public bool PredictedLabel { get; set; }
            [ColumnName("Probability")]
            public float Probability { get; set; }
        }
    }
}