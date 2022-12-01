using Microsoft.AspNetCore.Mvc;
using PreFinal1.ADO.NET;
using PreFinal1.Models;
using System.Data.SqlClient;

namespace PreFinal1.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class VentaController : ControllerBase
    {
        private VentaHandler handler = new VentaHandler();

        [HttpGet]
        public ActionResult<List<Venta>> Get()
        {
            try
            {
                List<Venta> lista = handler.GetVenta();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}
