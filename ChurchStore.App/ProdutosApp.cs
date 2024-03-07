using ChurchStore.Database.Repositorios;
using ChurchStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchStore.App
{
    public class ProdutosApplication
    {
        private readonly ProdutosRepositorio _produtosRepositorio;
        public ProdutosApplication(ProdutosRepositorio produtosRepositorio)
        {
            _produtosRepositorio = produtosRepositorio;
        }
        public async Task<List<Produto>> Listar()
        {
            try
            {
                return await _produtosRepositorio.Listar();

            }
            catch
            {
                throw;
            }
        }
    }
}
