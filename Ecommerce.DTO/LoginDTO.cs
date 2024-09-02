using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El correo es requerido")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "La clave es requerida")]
        public string? Clave { get; set; }
    }
}