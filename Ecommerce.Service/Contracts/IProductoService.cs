using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTO;

namespace Ecommerce.Service.Contracts
{
    public interface IProductoService
    {
        Task<List<ProductoDTO>> Lista(string buscar);
        Task<List<ProductoDTO>> Catalogo(string categoria, string buscar);
        Task<ProductoDTO> Obtener(int id);
        Task<ProductoDTO> Crear(ProductoDTO modelo);
        Task<bool> Editar(ProductoDTO modelo);
        Task<bool> Eliminar(int id);
    }
}