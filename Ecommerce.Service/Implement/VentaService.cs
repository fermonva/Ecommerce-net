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
    public class VentaService : IVentaService
    {
        private readonly IGenericoRepository<Venta> _ventaRepository;
        private readonly IMapper _mapper;


        public VentaService(IGenericoRepository<Venta> ventaRepository, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _mapper = mapper;
        }

        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try
            {
                var dbModelo = _mapper.Map<Venta>(modelo);
                var response = await _ventaRepository.CreateAsync(dbModelo);

                if (response.IdVenta == 0) throw new TaskCanceledException("No se pudo registrar la venta");

                return _mapper.Map<VentaDTO>(response);
            }
            catch (Exception ex)
            {
                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }
    }
}