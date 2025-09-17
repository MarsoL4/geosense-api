using GeoSense.API.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace GeoSense.API.Examples
{
    public class PatioDTOExample : IExamplesProvider<PatioDTO>
    {
        public PatioDTO GetExamples()
        {
            return new PatioDTO
            {
                Id = 1
            };
        }
    }
}