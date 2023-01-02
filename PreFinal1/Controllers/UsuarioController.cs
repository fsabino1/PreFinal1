using Microsoft.AspNetCore.Mvc;
using PreFinal1.ADO.NET;
using PreFinal1.Models;
using System.Data.SqlClient;


namespace PreFinal1.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private UsuarioHandler handler = new UsuarioHandler();

        [HttpPost]//Crear Usuario
        public ActionResult Post([FromBody] Usuario user)
        {
            try 
            {
                Usuario usuarioCreado = handler.crearUsuario(user);
                return StatusCode(StatusCodes.Status201Created, usuarioCreado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]//Modificar Usuario
        public ActionResult<Usuario> Put(int id, [FromBody] Usuario usuarioAModificar)
        {
            try
            {
                Usuario? usuarioActualizado = handler.actualizarUsuario(id, usuarioAModificar);
                return Ok(usuarioActualizado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]//Traer usuario desde  NombreUsuario
        public ActionResult<Usuario> Get(string nombreUser)
        {
            try
            {
                Usuario userMostrar = handler.obtenerUsuarioDsdeNU(nombreUser);
                return Ok(userMostrar);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]//Eliminar Usuario
        public ActionResult Delete([FromBody] int id)
        {
            try
            {
                bool seElimino = handler.eliminarUsuario(id);
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
}
