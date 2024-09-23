using ArticulosBack.Entities;
using ArticulosBack.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IAplicacionService _aplicacionService;
        public FacturaController()
        {
            _aplicacionService = new AplicacionService();
        }

        //GET
        [HttpGet("Facturas")]
        public IActionResult GetFactura()
        {

            return Ok(_aplicacionService.GetAllFacturas());

        }
        //GET
        [HttpGet("Consultar Articulos por parametros")]
        public IActionResult ObtenerFacturas([FromQuery] DateTime? fecha = null, [FromQuery] int? formaPagoId = null)
        {
            List<Factura> facturas = _aplicacionService.ConsultarFacturas(fecha, formaPagoId);
            if (facturas == null || facturas.Count == 0)
                return NotFound("No se encontraron facturas");
            else
                return Ok(facturas);
        }

        //POST
        [HttpPost]
        public IActionResult RegistarFactura([FromBody] Factura factura)
        {
            if (factura == null)
            {
                return BadRequest("Factura Nula");
            }
            bool creada = _aplicacionService.AgregarFactura(factura);
            if (creada)
            {
                return Ok("Factura Creada Con Éxito");
            }
            else
                return StatusCode(500, "Error al crear la Factura");
        }

        //PUT
        [HttpPut("{nroFactura}")]
        public IActionResult ActualizarFactura(int nroFactura, [FromBody] Factura factura)
        {
            if (factura == null)
                return BadRequest("La factura es nula");

            bool actualizar = _aplicacionService.ActualizarFactura(nroFactura, factura);
            if (actualizar)
                return Ok("Factura Actualizada Correctamente");
            else
                return StatusCode(500, "Error al actualizar la factura");

        }

    }
}
