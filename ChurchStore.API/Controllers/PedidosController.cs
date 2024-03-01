using ChurchStore.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChurchStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidosApplication _pedidosApplication;

        public PedidosController(PedidosApplication pedidosApplication)
        {
            _pedidosApplication = pedidosApplication;
        }

        [Route("listar")]
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var listaUsuarios = await _pedidosApplication.Listar();
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [Route("produtos/listar")]
        [HttpGet]
        public async Task<IActionResult> ListarProdutos()
        {
            try
            {
                var listaUsuarios = await _pedidosApplication.ListarProdutos();
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
