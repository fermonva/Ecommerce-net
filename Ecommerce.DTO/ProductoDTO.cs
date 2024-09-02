using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        public string? Descripcion { get; set; }

        public int? IdCategoria { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        public decimal? Precio { get; set; }

        [Required(ErrorMessage = "La oferta es requerida")]
        public decimal? PrecioOferta { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        public int? Cantidad { get; set; }

        [Required(ErrorMessage = "La imagen es requerida")]
        public string? Imagen { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public virtual CategoriaDTO? IdCategoriaNavigation { get; set; }
    }
}