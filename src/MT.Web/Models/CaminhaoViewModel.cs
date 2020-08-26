using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.Web.Models
{
    public class CaminhaoViewModel
    {
        public Guid Id { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public Guid ModeloId { get; set; }
    }
}
