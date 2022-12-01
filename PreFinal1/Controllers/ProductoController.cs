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

        [HttpGet]
        public ActionResult<List<Producto>> Get()
        {
            try
            {
                List<Producto> lista = handler.GetProductos();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}
