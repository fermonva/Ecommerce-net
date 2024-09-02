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
    public class DashboardService : IDashboardService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IGenericoRepository<Usuario> _usuarioRepository;
        private readonly IGenericoRepository<Producto> _productoRepository;


        public DashboardService(IVentaRepository ventaRepository, IGenericoRepository<Usuario> usuarioRepository, IGenericoRepository<Producto> productoRepository)
        {
            _ventaRepository = ventaRepository;
            _usuarioRepository = usuarioRepository;
            _productoRepository = productoRepository;

        }

        private string CantidadIngresos()
        {
            var result = _ventaRepository.GetFiltered();
            decimal? ingresos = result.Sum(v => v.Total);
            return ingresos.ToString() ?? "0";
        }

        private int CantidadVentas()
        {
            var result = _ventaRepository.GetFiltered();
            int? ventas = result.Count();
            return ventas ?? 0;
        }

        private int CantidadClientes()
        {
            var result = _usuarioRepository.GetFiltered(u => u.Rol!.ToLower() == "cliente");
            int? clientes = result.Count();
            return clientes ?? 0;
        }

        private int CantidadProductos()
        {
            var result = _productoRepository.GetFiltered();
            int? productos = result.Count();
            return productos ?? 0;
        }

        public DashboardDTO Resumen()
        {
            try
            {
                DashboardDTO dashboard = new()
                {
                    TotalIngresos = CantidadIngresos(),
                    TotalVentas = CantidadVentas(),
                    TotalClientes = CantidadClientes(),
                    TotalProductos = CantidadProductos(),
                };

                return dashboard;
            }
            catch (Exception ex)
            {
                throw new TaskCanceledException("Ocurrio un error", ex);
            }
        }
    }
}