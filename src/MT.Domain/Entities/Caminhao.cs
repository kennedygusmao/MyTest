using System;

namespace MT.Domain.Entities
{
    public class Caminhao : EntityBase
    {
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public virtual Guid ModeloId { get; set; }
        public virtual Modelo Modelo { get; set; }

        public Caminhao()
        {
        }

        public Caminhao(Guid id, int anoFabricacao, int anoModelo, DateTime creatAt, Modelo modelo)
        {
            Id = id;
            AnoFabricacao = anoFabricacao;
            AnoModelo = anoModelo;
            Modelo = modelo;
            CreateAt = creatAt;


        }
    }
}
