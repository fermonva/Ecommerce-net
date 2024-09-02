using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class TarjetaDTO
    {
        [Required(ErrorMessage = "El titular es requerido")]
        public string? Titular { get; set; }

        [Required(ErrorMessage = "El numero es requerido")]
        public string? Numero { get; set; }

        [Required(ErrorMessage = "La vigencia es requerida")]
        public string? Vigencia { get; set; }

        [Required(ErrorMessage = "El CVV es requerido")]
        public string? CVV { get; set; }
    }
}