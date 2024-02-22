﻿using ChurchStore.Database.Repositorios;
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

        public async Task<Usuario> Retornar(string login, string senha)
        {
            try
            {
                var lista = await _usuarioRepositorio.Retornar(login, senha);
                return lista;
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
