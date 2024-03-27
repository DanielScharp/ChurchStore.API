using ChurchStore.Database.Repositorios;
using ChurchStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchStore.App
{
    public class UsuarioApplication
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;
        public UsuarioApplication(UsuarioRepositorio leilaoRepositorio)
        {
            _usuarioRepositorio = leilaoRepositorio;
        }

        public async Task<Usuario> Retornar(string email, string senha = "")
        {
            try
            {
                var usuario = await _usuarioRepositorio.Retornar(email, senha);
                return usuario;
            }
            catch
            {
                throw;
            }
        }
        public void Cadastrar(Login user)
        {
            try
            {
                _usuarioRepositorio.Cadastrar(user);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Usuario>> ListarUsuarios()
        {
            try
            {
                var lista = await _usuarioRepositorio.ListarUsuarios();
                return lista;
            }
            catch
            {
                throw;
            }
        }
    }
}
