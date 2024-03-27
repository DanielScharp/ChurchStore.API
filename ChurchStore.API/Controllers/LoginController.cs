using ChurchStore.API.Services;
using ChurchStore.App;
using ChurchStore.Database.Repositorios;
using ChurchStore.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChurchStore.API.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UsuarioApplication _usuarioApplication;

        public LoginController(UsuarioApplication usuarioApplication)
        {
            _usuarioApplication = usuarioApplication;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] Login model)
        {
            var user = await _usuarioApplication.Retornar(model.Telefone, model.Senha);

            if (user.UsuarioId == 0)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }

            var token = TokenService.GenerateToken(user);

            return Ok(token);

        }
    }
}
