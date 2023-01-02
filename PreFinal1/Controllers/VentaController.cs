using Microsoft.AspNetCore.Mvc;
using PreFinal1.ADO.NET;
using PreFinal1.Models;
using System.Data.SqlClient;

namespace PreFinal1.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class VentaController : Controller
    {
        private VentaRepository repository = new VentaRepository();
        // Agregar Venta
        [HttpPost]
        public ActionResult Post([FromBody] Venta venta)
        {
            try
            {
                Venta ventaCreado = repository.cargarVenta(venta);
                return StatusCode(StatusCodes.Status201Created, ventaCreado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        // Eliminar Venta
        [HttpDelete]
        public ActionResult Delete([FromBody] long id)
        {
            try
            {
                bool seElimino = repository.eliminarVenta(id);
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
    }
