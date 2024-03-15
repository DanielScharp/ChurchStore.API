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

        [Route("itens/listar")]
        [HttpGet]
        public async Task<IActionResult> ListarItens(int clienteId)
        {
            try
            {
                var listaUsuarios = await _pedidosApplication.ListarItensPorCliente(clienteId);
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [Route("itens/adicionar")]
        [HttpPost]
        public async Task<IActionResult> AdicionarItemAoPedido(int clienteId, int produtoId, int quantidade)
        {
            try
            {
                var listaUsuarios = await _pedidosApplication.AdicionarItemAoPedido(clienteId, produtoId, quantidade);
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [Route("itens/remover")]
        [HttpPost]
        public async Task<IActionResult> RemoverItemDoPedido(int clienteId, int produtoId)
        {
            try
            {
                var removido = await _pedidosApplication.RemoverItemDoPedido(clienteId, produtoId);
                return Ok(removido);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
