using ChurchStore.Database.Repositorios;
using ChurchStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchStore.App
{
    public class PedidosApplication
    {
        private readonly PedidosRepositorio _pedidosRepositorio;
        public PedidosApplication(PedidosRepositorio pedidosRepositorio)
        {
            _pedidosRepositorio = pedidosRepositorio;
        }

        public async Task<List<Pedido>> Listar()
        {
            try
            {
                return await _pedidosRepositorio.Listar();
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Produto>> ListarProdutos()
        {
            try
            {
                return await _pedidosRepositorio.ListarProdutos();
                 
            }
            catch
            {
                throw;
            }
        }
    }
}
