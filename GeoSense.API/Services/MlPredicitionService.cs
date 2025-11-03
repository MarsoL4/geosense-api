using Microsoft.ML;
using Microsoft.ML.Data;

namespace GeoSense.API.Services
{
    /// <summary>
    /// Serviço de predição usando ML.NET para tempo médio de permanência.
    /// </summary>
    public class MlPredictionService
    {
        private readonly MLContext _mlContext;
        private readonly PredictionEngine<ModelInput, ModelOutput> _predictionEngine;

        public MlPredictionService()
        {
            _mlContext = new MLContext();

            // Pipeline de regressão - simples demonstração
            var pipeline = _mlContext.Transforms.Concatenate("Features", nameof(ModelInput.TotalMotos), nameof(ModelInput.VagasLivres), nameof(ModelInput.MotosComProblema))
                .Append(_mlContext.Regression.Trainers.Sdca(labelColumnName: "Label"));

            // Treino com dados fictícios
            var data = new List<ModelInput>
            {
                new() { TotalMotos = 10, VagasLivres = 4, MotosComProblema = 2, TempoMedio = 2.5f },
                new() { TotalMotos = 20, VagasLivres = 2, MotosComProblema = 4, TempoMedio = 4.5f },
                new() { TotalMotos = 5, VagasLivres = 8, MotosComProblema = 0, TempoMedio = 1.2f }
            };

            var mlData = _mlContext.Data.LoadFromEnumerable(data.Select(d =>
            {
                d.Label = d.TempoMedio;
                return d;
            }));

            var model = pipeline.Fit(mlData);

            _predictionEngine = _mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
        }

        public float PreverTempoMedio(int totalMotos, int vagasLivres, int motosComProblema)
        {
            var input = new ModelInput
            {
                TotalMotos = totalMotos,
                VagasLivres = vagasLivres,
                MotosComProblema = motosComProblema
            };
            var prediction = _predictionEngine.Predict(input);
            return Math.Max(0, prediction.Score); // não devolver valor negativo
        }

        public class ModelInput
        {
            public float TotalMotos { get; set; }
            public float VagasLivres { get; set; }
            public float MotosComProblema { get; set; }
            public float TempoMedio { get; set; }
            [ColumnName("Label")] public float Label { get; set; }
        }

        public class ModelOutput
        {
            public float Score { get; set; }
        }
    }
}