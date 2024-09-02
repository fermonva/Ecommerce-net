using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string? NombreCompleto { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "La clave es requerida")]
        public string? Clave { get; set; }

        [Compare("Clave", ErrorMessage = "Las claves no coinciden")]
        public string? ConfirmarClave { get; set; }

        public string? Rol { get; set; }
    }
}