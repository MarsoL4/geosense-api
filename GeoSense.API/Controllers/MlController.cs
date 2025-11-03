using GeoSense.API.DTOs.ML;
using GeoSense.API.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GeoSense.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ml")]
    [ApiController]
    public class MlController(MlPredictionService svc) : ControllerBase
    {
        private readonly MlPredictionService _svc = svc;

        /// <summary>
        /// Realiza a predição de necessidade de manutenção de uma moto usando ML.NET.
        /// </summary>
        /// <remarks>
        /// Forneça dados da moto/vaga para obter se a moto precisa de manutenção segundo o modelo.
        /// </remarks>
        /// <param name="input">Informações da moto/vaga</param>
        /// <response code="200">Resultado da predição</response>
        [HttpPost("predict-maintenance")]
        [SwaggerResponse(200, "Resultado da predição", typeof(PredictMaintenanceResultDTO))]
        public ActionResult<PredictMaintenanceResultDTO> PredictMaintenance([FromBody] PredictMaintenanceDTO input)
        {
            var result = _svc.Predict(input);
            return Ok(result);
        }
    }
}