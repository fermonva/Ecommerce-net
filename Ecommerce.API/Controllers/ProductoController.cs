using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTO;
using Ecommerce.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly IProductoService _productoService;

        public ProductoController(ILogger<ProductoController> logger, IProductoService productoService)
        {
            _logger = logger;
            _productoService = productoService;
        }

        [HttpGet("Lista/{buscar?}")]
        public async Task<IActionResult> Lista(string? buscar = "")
        {
            var response = new ResponseDTO<List<ProductoDTO>>();
            try
            {
                if (string.IsNullOrEmpty(buscar)) buscar = "";
                response.Success = true;
                response.Result = await _productoService.Lista(buscar);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                _logger.LogError(ex, "Error al obtener la lista de productos");
            }
            return Ok(response);
        }

        [HttpGet("Catalogo/{categoria}/{buscar?}")]
        public async Task<IActionResult> Catalogo(string categoria, string? buscar = "")
        {
            var response = new ResponseDTO<List<ProductoDTO>>();
            try
            {
                if (categoria.ToLower() == "todos") categoria = "";
                if (string.IsNullOrEmpty(buscar)) buscar = "";

                response.Success = true;
                response.Result = await _productoService.Catalogo(categoria, buscar);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                _logger.LogError(ex, "Error al obtener la lista de productos");
            }
            return Ok(response);
        }

        [HttpGet("Obtener/{id:int}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var response = new ResponseDTO<ProductoDTO>();
            try
            {
                response.Success = true;
                response.Result = await _productoService.Obtener(id);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                _logger.LogError(ex, "Error al obtener la lista de productos");
            }
            return Ok(response);
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody] ProductoDTO modelo)
        {
            var response = new ResponseDTO<ProductoDTO>();
            try
            {
                response.Success = true;
                response.Result = await _productoService.Crear(modelo);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                _logger.LogError(ex, "Error al crear el producto");
            }
            return Ok(response);
        }

        [HttpPut("Editar")]
        public async Task<IActionResult> Editar([FromBody] ProductoDTO modelo)
        {
            var response = new ResponseDTO<bool>();
            try
            {
                response.Success = true;
                response.Result = await _productoService.Editar(modelo);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                _logger.LogError(ex, "Error al actualizar el producto");
            }
            return Ok(response);
        }

        [HttpDelete("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var response = new ResponseDTO<bool>();
            try
            {
                response.Success = true;
                response.Result = await _productoService.Eliminar(id);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                _logger.LogError(ex, "Error al eliminar la categoria");
            }
            return Ok(response);
        }
    }
}