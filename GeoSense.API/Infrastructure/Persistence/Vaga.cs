using GeoSense.API.Domain;
using GeoSense.API.Domain.Enums;

namespace GeoSense.API.Infrastructure.Persistence
{
    public class Vaga : Audit
    {
        public Guid Id { get; private set; }
        public int Numero { get; private set; }
        public TipoVaga Tipo { get; private set; }
        public StatusVaga Status { get; private set; }

        //N...1
        public Guid PatioId { get; private set; }
        public virtual Patio Patio { get; set; }

        protected Vaga() { }

        public Vaga(int numero, Guid patio_id)
        {
            Id = Guid.NewGuid();
            Numero = numero;
            Tipo = TipoVaga.Sem_Problema;
            Status = StatusVaga.LIVRE;
            PatioId = patio_id;
        }

        public ICollection<Moto> Motos { get; set; } = new List<Moto>();
        public ICollection<AlocacaoMoto> Alocacoes { get; set; } = new List<AlocacaoMoto>();

    }
}
