using ChurchStore.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchStore.Database.Repositorios
{
    public class ProdutosRepositorio
    {
        private readonly string _connMySql;

        public ProdutosRepositorio(string connMySql)
        {
            _connMySql = connMySql;
        }

        public async Task<List<Produto>> Listar()
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT * ");
                    sql.Append(" FROM produtos ");

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    var pedidos = new List<Produto>();

                    while (reader.Read())
                    {
                        var pedido = new Produto();

                        pedido.ProdutoId = reader.GetInt32(reader.GetOrdinal("ProdutoId"));
                        pedido.ProdutoNome = reader[reader.GetOrdinal("ProdutoNome")].ToString();
                        pedido.ProdutoValor = reader[reader.GetOrdinal("ProdutoValor")] != DBNull.Value ? reader.GetDouble(reader.GetOrdinal("ProdutoValor")) : 0;
                        pedido.Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"));
                        pedido.ImagemUrl = reader[reader.GetOrdinal("ImagemUrl")].ToString();

                        pedidos.Add(pedido);
                    }

                    return pedidos;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
