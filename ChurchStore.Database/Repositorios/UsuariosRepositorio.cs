using ChurchStore.Domain;
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

        public async Task<Usuario> Retornar(string login, string senha)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT * FROM church_shop.usuarios ");
                    sql.AppendFormat(" where login = '{0}' and senha = MD5('{1}') ", login, senha);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    var usuario = new Usuario();

                    if (reader.Read())
                    {

                        usuario.UsuarioId = reader.GetInt32(reader.GetOrdinal("UsuarioId"));
                        usuario.Nome = reader[reader.GetOrdinal("Nome")].ToString();
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
                    sql.Append(" SELECT * FROM church_shop.usuarios; ");

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
    }
}
