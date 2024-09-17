using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArticulosBack.Services;
using ArticulosBack.Entities;
using ApiProductos.Models;

namespace ApiProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        #region Instacia
        private IArticuloServices service;
        public ArticuloController()
        {
            service = new ArticuloManager();
        }
        #endregion
        //GET
        [HttpGet("Artículos")]
        public IActionResult GetArti()
        {
            return Ok(service.GetAllArticulos());
        }

        //POST
        [HttpPost]
        public IActionResult AddArticulo([FromBody] ArticuloModels articuloM)
        {
            if (articuloM == null || string.IsNullOrEmpty(articuloM.Nombre) || articuloM.PrecioUnitario <= 0)
            {
                return BadRequest("Datos inválidos");
            }
            var articulo = new Articulo
            {
                Nombre = articuloM.Nombre,
                PrecioUnitario = articuloM.PrecioUnitario
            };

            bool agregado = service.AddArticulos(articulo);

            if (agregado)
            {
                return Ok("Artículo agregado con éxito.");
            }
            else
            {
                return StatusCode(500, "Error al agregar el artículo.");
            }
        }

        //PUT
        [HttpPut("{id}")]

        public IActionResult UpdateArticulo(int id, [FromBody] ArticuloModels model)
        {
            // Validación inicial
            if (model == null || string.IsNullOrEmpty(model.Nombre) || model.PrecioUnitario <= 0)
            {
                return BadRequest("Datos del artículo inválidos.");
            }

            // Lógica para mapear el modelo a la entidad
            var articulo = new Articulo
            {
                Id = id,
                Nombre = model.Nombre,
                PrecioUnitario = model.PrecioUnitario
            };

            // Intentamos actualizar el artículo en el repositorio
            bool actualizado = service.UpdateArticulos(articulo);

            if (actualizado)
            {
                return Ok("Artículo actualizado con éxito.");
            }
            else
            {
                return StatusCode(500, "Error al actualizar el artículo.");
            }
        }
        //DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteArticulo(int id)
        {
            bool borrado = service.DeleteArticulos(id);

            if (borrado)
            {
                return Ok("Artículo eliminado con éxito.");
            }
            else
            {
                return StatusCode(500, "Error al eliminar el artículo.");
            }
        }
    }
}
