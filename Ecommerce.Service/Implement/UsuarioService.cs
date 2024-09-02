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
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericoRepository<Usuario> _usuarioRepository;
        private readonly IMapper _mapper;


        public UsuarioService(IGenericoRepository<Usuario> usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }


        public async Task<SesionDTO> Autorizacion(LoginDTO modelo)
        {
            try
            {
                var usuario = _usuarioRepository.GetFiltered(u => u.Correo == modelo.Correo && u.Clave == modelo.Clave);
                var fromDbModelo = await usuario.FirstOrDefaultAsync();

                if (fromDbModelo != null) return _mapper.Map<SesionDTO>(fromDbModelo);
                else throw new TaskCanceledException("Credenciales incorrectas");
            }
            catch (Exception ex)
            {
                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }

        public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try
            {
                var dbModelo = _mapper.Map<Usuario>(modelo);
                var response = await _usuarioRepository.CreateAsync(dbModelo);

                if (response.IdUsuario != 0) return _mapper.Map<UsuarioDTO>(response);
                else throw new TaskCanceledException("No se pudo registrar el usuario");
            }
            catch (Exception ex)
            {
                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }

        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            try
            {
                var result = _usuarioRepository.GetFiltered(u => u.IdUsuario == modelo.IdUsuario);
                var fromDbModelo = await result.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                {
                    fromDbModelo.NombreCompleto = modelo.NombreCompleto;
                    fromDbModelo.Correo = modelo.Correo;
                    fromDbModelo.Clave = modelo.Clave;

                    var response = await _usuarioRepository.UpdateAsync(fromDbModelo);
                    if (!response) throw new TaskCanceledException("No se pudo actualizar el usuario");
                    return response;
                }
                else throw new TaskCanceledException("No se encontro el usuario");
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
                var result = _usuarioRepository.GetFiltered(u => u.IdUsuario == id);
                var fromDbModelo = await result.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                {
                    var response = await _usuarioRepository.DeleteAsync(fromDbModelo);
                    if (!response) throw new TaskCanceledException("No se pudo eliminar el usuario");
                    return response;
                }
                else throw new TaskCanceledException("No se encontro el usuario");
            }
            catch (Exception ex)
            {

                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }

        public async Task<List<UsuarioDTO>> Lista(string rol, string buscar = "")
        {
            try
            {
                var result = _usuarioRepository.GetFiltered(u => u.Rol!.ToLower() == rol);

                if (!string.IsNullOrWhiteSpace(buscar))
                {
                    result = result.Where(u => u.NombreCompleto!.ToLower().Contains(buscar.ToLower()));
                }

                List<UsuarioDTO> lista = _mapper.Map<List<UsuarioDTO>>(await result.ToListAsync());

                return lista;
            }
            catch (Exception ex)
            {
                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }

        public async Task<UsuarioDTO> Obtener(int id)
        {
            try
            {
                var result = _usuarioRepository.GetFiltered(u => u.IdUsuario == id);
                var fromDbModelo = await result.FirstOrDefaultAsync();

                if (fromDbModelo != null) return _mapper.Map<UsuarioDTO>(fromDbModelo);
                else throw new TaskCanceledException("No se encontro el usuario");
            }
            catch (Exception ex)
            {

                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }
    }
}