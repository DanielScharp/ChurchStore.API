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
                var listapedidos = await _pedidosRepositorio.Listar();
                return listapedidos;
            }
            catch
            {
                throw;
            }
        }
    }
}
