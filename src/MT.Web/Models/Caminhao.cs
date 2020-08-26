using System;
using System.ComponentModel.DataAnnotations;

namespace MT.Web.Models
{
    public class Caminhao
    {
     
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo ano fabricação é obrigatório")]
        [RegularExpression(@"^[0-9999]*$", ErrorMessage = "Somente números")]
        public int AnoFabricacao { get; set; }

        [Required(ErrorMessage = "O campo ano modelo é obrigatório")]
        [RegularExpression(@"^[0-9999]*$",ErrorMessage = "Somente números")]        
        public int AnoModelo { get; set; }

        [Required(ErrorMessage = "O campo ano ModeloId é obrigatório")]
        public Guid ModeloId { get; set; }

        
    }
}
