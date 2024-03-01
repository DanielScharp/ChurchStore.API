using ChurchStore.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchStore.Database.Repositorios
{
    public class PedidosRepositorio
    {
        private readonly string _connMySql;

        public PedidosRepositorio(string connMySql)
        {
            _connMySql = connMySql;
        }

        public async Task<List<Pedido>> Listar()
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT t1.*, t2.Status, T3.* ");
                    sql.Append(" FROM pedidos t1");
                    sql.Append(" LEFT JOIN pedidos_status t2 ON t2.StatusId = t1.StatusId ");
                    sql.Append(" LEFT JOIN clientes t3 ON t3.ClienteId = t1.ClienteId ");

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    var pedidos = new List<Pedido>();

                    while (reader.Read())
                    {
                        var pedido = new Pedido();

                        pedido.PedidoId = reader.GetInt32(reader.GetOrdinal("PedidoId"));
                        pedido.ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId"));
                        pedido.ClienteNome = reader[reader.GetOrdinal("ClienteNome")].ToString();
                        pedido.ClienteTel = reader[reader.GetOrdinal("Telefone")].ToString();
                        pedido.StatusId = reader.GetInt32(reader.GetOrdinal("StatusId"));
                        pedido.StatusNome = reader[reader.GetOrdinal("Status")].ToString();
                        pedido.PedidoData = reader[reader.GetOrdinal("PedidoData")] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("PedidoData")) : new DateTime();
                        pedido.PedidoValor = reader[reader.GetOrdinal("PedidoValor")] != DBNull.Value ? reader.GetDouble(reader.GetOrdinal("PedidoValor")) : 0;

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
        public async Task<List<Produto>> ListarProdutos()
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
