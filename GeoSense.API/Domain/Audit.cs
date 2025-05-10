using GeoSense.API.Domain.Enums;

namespace GeoSense.API.Domain
{
    public class Audit
    {
        public StatusVaga Status { get; protected set; }
        public TipoVaga Tipo { get; protected set; }
    }
}
