using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTO;

namespace Ecommerce.WebAssembly.Services.Contracts
{
    public interface IDashboardService
    {
        Task<ResponseDTO<DashboardDTO>> Resumen();
    }
}