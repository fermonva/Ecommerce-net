using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model;

namespace Ecommerce.Repository.Contracts
{
    public interface IVentaRepository : IGenericoRepository<Venta>
    {
        Task<Venta> RegisterSale(Venta modelo);

    }
}