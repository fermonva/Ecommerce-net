using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model;
using Ecommerce.Repository.Contracts;
using Ecommerce.Repository.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Implement
{
    public class VentaRepository : GenericoRepository<Venta>, IVentaRepository
    {
        private readonly DBEcommerceContext _dbcontext;

        public VentaRepository(DBEcommerceContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<Venta> RegisterSale(Venta modelo)
        {
            Venta ventaGenerada = new Venta();

            using (var transaction = await _dbcontext.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (DetalleVenta item in modelo.DetalleVenta)
                    {
                        Producto product = _dbcontext.Productos.Where(p => p.IdProducto == item.IdProducto).First();

                        product.Cantidad -= item.Cantidad;
                        _dbcontext.Productos.Update(product);
                    }

                    await _dbcontext.SaveChangesAsync();
                    await _dbcontext.Venta.AddAsync(modelo);
                    await _dbcontext.SaveChangesAsync();
                    ventaGenerada = modelo;
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            return ventaGenerada;
        }
    }
}