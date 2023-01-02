using Microsoft.AspNetCore.Mvc;
using PreFinal1.ADO.NET;
using PreFinal1.Models;
using System.Data.SqlClient;

namespace PreFinal1.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private ProductoHandler handler = new ProductoHandler();

        [HttpPost]
        public ActionResult Post([FromBody] Producto producto)
        {
            try
            {
                Producto productoCreado = handler.crearProducto(producto);
                return StatusCode(StatusCodes.Status201Created, productoCreado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        // Modificar Producto
        [HttpPut]
        public ActionResult<Producto> Put(long id, [FromBody] Producto prductoAActualizar)
        {
            try
            {
                Producto? productoActualizado = handler.modificarProductoDesdeId(id, prductoAActualizar);

                return Ok(productoActualizado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // Eliminar producto
        [HttpDelete]

        public ActionResult Delete([FromBody] long id)
        {
            try
            {
                bool seElimino = handler.eliminarProducto(id);
                if (seElimino)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //Traer todos los productos cargados
        [HttpGet]//listo productos
        public ActionResult<List<Producto>> Get()
        {
            try
            {
                List<Producto> lista = handler.listarProductos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
