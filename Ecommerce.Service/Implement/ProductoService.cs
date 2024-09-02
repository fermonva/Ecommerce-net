using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Ecommerce.Model;
using Ecommerce.DTO;
using Ecommerce.Repository.Contracts;
using Ecommerce.Service.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Service.Implement
{
    public class ProductoService : IProductoService
    {
        private readonly IGenericoRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;


        public ProductoService(IGenericoRepository<Producto> productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> Catalogo(string categoria, string buscar)
        {
            try
            {
                var result = _productoRepository.GetFiltered(p => p.Nombre!.ToLower().Contains(buscar.ToLower()) && p.IdCategoriaNavigation!.Nombre!.ToLower().Contains(categoria.ToLower()));

                List<ProductoDTO> lista = _mapper.Map<List<ProductoDTO>>(await result.ToListAsync());

                return lista;
            }
            catch (Exception ex)
            {
                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }

        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var dbModelo = _mapper.Map<Producto>(modelo);
                var response = await _productoRepository.CreateAsync(dbModelo);

                if (response.IdProducto != 0) return _mapper.Map<ProductoDTO>(response);
                else throw new TaskCanceledException("No se pudo registrar el producto");
            }
            catch (Exception ex)
            {
                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var result = _productoRepository.GetFiltered(p => p.IdProducto == modelo.IdProducto);
                var fromDbModelo = await result.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                {
                    fromDbModelo.Nombre = modelo.Nombre;
                    fromDbModelo.Descripcion = modelo.Descripcion;
                    fromDbModelo.IdCategoria = modelo.IdCategoria;
                    fromDbModelo.Precio = modelo.Precio;
                    fromDbModelo.PrecioOferta = modelo.PrecioOferta;
                    fromDbModelo.Cantidad = modelo.Cantidad;
                    fromDbModelo.Imagen = modelo.Imagen;

                    var response = await _productoRepository.UpdateAsync(fromDbModelo);
                    if (!response) throw new TaskCanceledException("No se pudo actualizar el producto");
                    return response;
                }
                else throw new TaskCanceledException("No se encontro el producto");
            }
            catch (Exception ex)
            {

                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var result = _productoRepository.GetFiltered(p => p.IdProducto == id);
                var fromDbModelo = await result.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                {
                    var response = await _productoRepository.DeleteAsync(fromDbModelo);
                    if (!response) throw new TaskCanceledException("No se pudo eliminar el producto");
                    return response;
                }
                else throw new TaskCanceledException("No se encontro el producto");
            }
            catch (Exception ex)
            {

                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }

        public async Task<List<ProductoDTO>> Lista(string buscar)
        {
            try
            {
                var result = _productoRepository.GetFiltered(p => p.Nombre!.ToLower().Contains(buscar.ToLower()));
                result = result.Include(c => c.IdCategoriaNavigation);

                List<ProductoDTO> lista = _mapper.Map<List<ProductoDTO>>(await result.ToListAsync());

                return lista;
            }
            catch (Exception ex)
            {

                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }

        public async Task<ProductoDTO> Obtener(int id)
        {
            try
            {
                var result = _productoRepository.GetFiltered(p => p.IdProducto == id);
                result = result.Include(c => c.IdCategoriaNavigation);

                var fromDbModelo = await result.FirstOrDefaultAsync();

                if (fromDbModelo != null) return _mapper.Map<ProductoDTO>(fromDbModelo);
                else throw new TaskCanceledException("No se encontro el producto");
            }
            catch (Exception ex)
            {
                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }
    }
}