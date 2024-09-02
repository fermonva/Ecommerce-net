using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTO;
using Ecommerce.Repository.Contracts;
using Ecommerce.Service.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly ILogger<VentaController> _logger;
        private readonly IVentaService _ventaService;

        public VentaController(ILogger<VentaController> logger, IVentaService ventaService)
        {
            _logger = logger;
            _ventaService = ventaService;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] VentaDTO modelo)
        {
            var response = new ResponseDTO<VentaDTO>();
            try
            {
                response.Success = true;
                response.Result = await _ventaService.Registrar(modelo);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                _logger.LogError(ex, "Error al crear el producto");
            }
            return Ok(response);
        }
    }
}