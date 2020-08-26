
using System;

namespace MT.Domain.Dtos
{
    public class CaminhaoDetalheDto
    {
        public Guid Id { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public string Modelo { get; set; }
        public Guid ModeloId { get; set; }
    }
}
