namespace GeoSense.API.Infrastructure.Persistence
{
    public class Moto
    {
        public Guid Id { get; private set; }
        public string Modelo { get; private set; }
        public string Placa { get; private set; }
        public string Chassi { get; private set; }
        public string Problema_Identificado { get; private set; }

        // 1..1
        public Guid VagaId { get; private set; }
        public virtual Vaga Vaga { get; set; }


        public Moto(Guid id, string modelo, string placa, string chassi, string problema_identificado, Guid vaga_id)
        {
            Id = Guid.NewGuid();
            Modelo = modelo;
            Placa = placa;
            Chassi = chassi;
            Problema_Identificado = problema_identificado;
            VagaId= vaga_id;
        }


    }
}
