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
    public class CategoriaService : ICategoriaService
    {
        private readonly IGenericoRepository<Categoria> _categoriaRepository;
        private readonly IMapper _mapper;


        public CategoriaService(IGenericoRepository<Categoria> categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<CategoriaDTO> Crear(CategoriaDTO modelo)
        {
            try
            {
                var dbModelo = _mapper.Map<Categoria>(modelo);
                var response = await _categoriaRepository.CreateAsync(dbModelo);

                if (response.IdCategoria != 0) return _mapper.Map<CategoriaDTO>(response);
                else throw new TaskCanceledException("No se pudo registrar la categoria");
            }
            catch (Exception ex)
            {
                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }

        public async Task<bool> Editar(CategoriaDTO modelo)
        {
            try
            {
                var result = _categoriaRepository.GetFiltered(c => c.IdCategoria == modelo.IdCategoria);
                var fromDbModelo = await result.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                {
                    fromDbModelo.Nombre = modelo.Nombre;

                    var response = await _categoriaRepository.UpdateAsync(fromDbModelo);
                    if (!response) throw new TaskCanceledException("No se pudo actualizar la categoria");
                    return response;
                }
                else throw new TaskCanceledException("No se encontro la categoria");
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
                var result = _categoriaRepository.GetFiltered(c => c.IdCategoria == id);
                var fromDbModelo = await result.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                {
                    var response = await _categoriaRepository.DeleteAsync(fromDbModelo);
                    if (!response) throw new TaskCanceledException("No se pudo eliminar la categoria");
                    return response;
                }
                else throw new TaskCanceledException("No se encontro la categoria");
            }
            catch (Exception ex)
            {

                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }

        public async Task<List<CategoriaDTO>> Lista(string buscar)
        {
            try
            {
                var result = _categoriaRepository.GetFiltered(c => c.Nombre!.ToLower().Contains(buscar.ToLower()));

                List<CategoriaDTO> lista = _mapper.Map<List<CategoriaDTO>>(await result.ToListAsync());

                return lista;
            }
            catch (Exception ex)
            {

                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }

        public async Task<CategoriaDTO> Obtener(int id)
        {
            try
            {
                var result = _categoriaRepository.GetFiltered(c => c.IdCategoria == id);
                var fromDbModelo = await result.FirstOrDefaultAsync();

                if (fromDbModelo != null) return _mapper.Map<CategoriaDTO>(fromDbModelo);
                else throw new TaskCanceledException("No se encontro la categoria");
            }
            catch (Exception ex)
            {

                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }
    }
}