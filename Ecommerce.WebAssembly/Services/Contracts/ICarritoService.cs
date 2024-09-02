using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTO;

namespace Ecommerce.WebAssembly.Services.Contracts
{
    public interface ICarritoService
    {
        event Action MostrarItemsCarrito;

        int CantidadProductos();
        Task AgregarCarrito(CarritoDTO modelo);
        Task EliminarCarrito(int id);
        Task<List<CarritoDTO>> ObtenerCarrito();
        Task VaciarCarrito();
    }
}