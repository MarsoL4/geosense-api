using GeoSense.API.Domain;
using GeoSense.API.Domain.Enums;

namespace GeoSense.API.Infrastructure.Persistence
{
    public class Vaga : Audit
    {
        public Guid Id { get; private set; }
        public int Numero { get; private set; }

        //N...1
        public Guid PatioId { get; private set; }
        public virtual Patio Patio { get; set; }

        public Vaga(Guid id, int numero, Guid patio_id)
        {
            Id = Guid.NewGuid();
            Numero = numero;
            Tipo = TipoVaga.Sem_Problema;
            Status = StatusVaga.LIVRE;
            PatioId = patio_id;
        }

    }
}
