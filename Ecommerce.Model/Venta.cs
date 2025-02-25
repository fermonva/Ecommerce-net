﻿using System;
using System.Collections.Generic;

namespace Ecommerce.Model
{
    public partial class Venta
    {
        public Venta()
        {
            DetalleVenta = new HashSet<DetalleVenta>();
        }

        public int IdVenta { get; set; }
        public int? IdUsuario { get; set; }
        public decimal? Total { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();
    }
}
