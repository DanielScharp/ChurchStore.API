﻿using ChurchStore.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchStore.Database.Repositorios
{
    public class UsuarioRepositorio
    {
        private readonly string _connMySql;

        public UsuarioRepositorio(string connMySql)
        {
            _connMySql = connMySql;
        }

        public async Task<Usuario> Retornar(string email, string senha = "")
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT * FROM church_store.usuarios ");
                    sql.AppendFormat(" where email = '{0}' ", email);
                    if(!String.IsNullOrEmpty(senha))
                    {
                        sql.AppendFormat(" and senha = MD5('{0}') ", senha);
                    }

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    var usuario = new Usuario();

                    if (reader.Read())
                    {

                        usuario.UsuarioId = reader.GetInt32(reader.GetOrdinal("UsuarioId"));
                        usuario.Nome = reader[reader.GetOrdinal("Nome")].ToString();
                        usuario.Email = reader[reader.GetOrdinal("Email")].ToString();
                    }

                    return usuario;
                }
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
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT * FROM church_store.usuarios; ");

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    var listaUsuarios = new List<Usuario>();


                    while (reader.Read())
                    {
                        var usuario = new Usuario();

                        usuario.UsuarioId = reader.GetInt32(reader.GetOrdinal("UsuarioId"));
                        usuario.Nome = reader[reader.GetOrdinal("Nome")].ToString();

                        listaUsuarios.Add(usuario);
                    }

                    return listaUsuarios;
                }
            }
            catch
            {
                throw;
            }
        }

        public async void Cadastrar(Login user)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" insert into usuarios ");
                    sql.Append(" (Nome, Email, Senha) ");
                    sql.Append(" values ");
                    sql.AppendFormat(" ('{0}','{1}', md5('{2}')) ", user.Nome, user.Email, user.Senha);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
