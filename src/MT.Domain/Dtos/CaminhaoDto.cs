
using System;

namespace MT.Domain.Dtos
{
    public class CaminhaoDto
    {
        public Guid Id { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }     
        public Guid ModeloId { get; set; }
    }
}