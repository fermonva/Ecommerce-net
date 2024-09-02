using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTO;

namespace Ecommerce.Service.Contracts
{
    public interface IDashboardService
    {
        DashboardDTO Resumen();
    }
}