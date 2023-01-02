using Microsoft.AspNetCore.Mvc;
using PreFinal1.ADO.NET;
using PreFinal1.Models;
using System.Data.SqlClient;

namespace PreFinal1.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        private ProductoVendidoHandler handler = new ProductoVendidoHandler();

        [HttpPost]
        public ActionResult Post([FromBody] ProductoVendido productoV)
        {
            try
            {
                ProductoVendido productoVendido = handler.cargarProductosVendidos(productoV);
                return StatusCode(StatusCodes.Status201Created, productoVendido);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        //eliminar producto vendido desde IdVenta
        [HttpDelete]
        public ActionResult Delete([FromBody] long idVta)
        {
            try
            {
                bool seElimino = handler.eliminarProductoVendido(idVta);
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
        [HttpGet]//listo productos
        public ActionResult<List<ProductoVendido>> Get(long idVenta)
        {
            try
            {
                List<ProductoVendido> lista = handler.obtenerProdVendidoIdVta(idVenta);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
