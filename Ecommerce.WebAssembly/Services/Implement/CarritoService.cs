using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Ecommerce.DTO;
using Ecommerce.WebAssembly.Services.Contracts;

namespace Ecommerce.WebAssembly.Services.Implement
{
    public class CarritoService : ICarritoService
    {

        private readonly ILocalStorageService _localStorageService;
        private readonly ISyncLocalStorageService _syncLocalStorageService;
        private readonly IToastService _toastService;
        public CarritoService(ILocalStorageService localStorageService, ISyncLocalStorageService syncLocalStorageService, IToastService toastService)
        {
            _localStorageService = localStorageService;
            _syncLocalStorageService = syncLocalStorageService;
            _toastService = toastService;
        }

        public event Action MostrarItemsCarrito = () => { };

        public async Task AgregarCarrito(CarritoDTO modelo)
        {
            try
            {
                var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito") ?? new List<CarritoDTO>();

                var encontrado = carrito.FirstOrDefault(c => c.Producto!.IdProducto == modelo.Producto!.IdProducto);
                if (encontrado != null)
                    carrito.Remove(encontrado);

                carrito.Add(modelo);

                await _localStorageService.SetItemAsync("carrito", carrito);
                if (encontrado != null)
                    _toastService.ShowSuccess("El producto ya se encuentra en el carrito");
                else
                    _toastService.ShowSuccess("Se agrego el producto al carrito");

                MostrarItemsCarrito.Invoke();
            }
            catch
            {
                _toastService.ShowError("No se pudo agregar al carrito");
            }
        }

        public int CantidadProductos()
        {
            var carrito = _syncLocalStorageService.GetItem<List<CarritoDTO>>("carrito");
            return carrito?.Count ?? 0;
        }

        public async Task<List<CarritoDTO>> ObtenerCarrito()
        {
            return await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito") ?? new List<CarritoDTO>();
        }
        public async Task EliminarCarrito(int id)
        {
            try
            {
                var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");
                if (carrito != null)
                {
                    var encontrado = carrito.FirstOrDefault(c => c.Producto!.IdProducto == id);
                    if (encontrado != null)
                        carrito.Remove(encontrado);
                    await _localStorageService.SetItemAsync("carrito", carrito);
                    _toastService.ShowSuccess("Se elimino el producto del carrito");
                    MostrarItemsCarrito.Invoke();
                }
            }
            catch
            {
                _toastService.ShowError("No se pudo eliminar del carrito");
            }
        }


        public async Task VaciarCarrito()
        {
            await _localStorageService.RemoveItemAsync("carrito");
            MostrarItemsCarrito.Invoke();
        }
    }
}