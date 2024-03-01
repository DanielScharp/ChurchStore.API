using ChurchStore.App;
using ChurchStore.Domain;
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

        [Route("cadastrar")]
        [HttpPost]
        public void Cadastrar(Login user)
        {
            try
            {
                 _usuarioApplication.Cadastrar(user);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
