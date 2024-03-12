using ChurchStore.App;
using ChurchStore.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChurchStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutosApplication _produtosApplication;

        public ProdutosController(ProdutosApplication produtosApplication)
        {
            _produtosApplication = produtosApplication;
        }

        [Route("listar")]
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var listaUsuarios = await _produtosApplication.Listar();
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [Route("adicionar")]
        [HttpPost]
        public async Task<IActionResult> AdicionarProduto(Produto produto)
        {
            try
            {
                var listaUsuarios = await _produtosApplication.AdicionarProduto(produto);
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
