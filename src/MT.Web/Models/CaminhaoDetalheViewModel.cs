using System;


namespace MT.Web.Models
{
    public class CaminhaoDetalheViewModel
    {
        public Guid Id { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public string Modelo { get; set; }

        
    }
}
