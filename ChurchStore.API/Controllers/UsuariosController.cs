using ChurchStore.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChurchStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioApplication _usuarioApplication;

        public UsuariosController(UsuarioApplication usuarioApplication)
        {
            _usuarioApplication = usuarioApplication;
        }

        [Route("listar")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var listaUsuarios = await _usuarioApplication.ListarUsuarios();
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
