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

        public async Task<List<Pedido>> Retornar()
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
        public async Task<List<PedidoItem>> ListarItensPorCliente(int clienteId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT t1.PedidoId, SUM(t1.Quantidade) as 'Quantidade', SUM(t1.Quantidade * t2.ProdutoValor) as 'Total', ");
                    sql.Append(" t2.ProdutoNome, t2.ProdutoValor, t2.ImagemUrl, t3.ClienteNome ");
                    sql.Append(" FROM pedidos_itens t1 ");
                    sql.Append(" left join produtos t2 on t2.ProdutoId = t1.ProdutoId ");
                    sql.Append(" LEFT JOIN clientes t3 ON t3.ClienteId = t1.ClienteId ");
                    sql.AppendFormat(" where t1.ClienteId ='{0}' ", clienteId);
                    sql.Append(" GROUP BY t1.PedidoId, t1.ProdutoId");

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    var pedidos = new List<PedidoItem>();

                    while (reader.Read())
                    {
                        var pedido = new PedidoItem();

                        pedido.PedidoId = reader.GetInt32(reader.GetOrdinal("PedidoId"));
                        pedido.ClienteNome = reader[reader.GetOrdinal("ClienteNome")].ToString();
                        pedido.Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"));
                        pedido.ProdutoNome = reader[reader.GetOrdinal("ProdutoNome")].ToString();
                        pedido.ImagemUrl = reader[reader.GetOrdinal("ImagemUrl")].ToString();
                        pedido.ProdutoValor = reader[reader.GetOrdinal("ProdutoValor")] != DBNull.Value ? reader.GetDouble(reader.GetOrdinal("ProdutoValor")) : 0;
                        pedido.Total = reader[reader.GetOrdinal("Total")] != DBNull.Value ? reader.GetDouble(reader.GetOrdinal("Total")) : 0;

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


        private async Task<ulong> AdicionarPedido(int clienteId)
        {
            try
            {

                ulong lastInsertedId = 0;
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" INSERT INTO pedidos ");
                    sql.Append(" (`ClienteId`, `StatusId`, `PedidoData`) ");
                    sql.Append(" VALUES ");
                    sql.AppendFormat(" ('{0}', '{1}', '{2}'); ", clienteId, 1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    sql.Append(" SELECT LAST_INSERT_ID();"); // Adicionando a consulta para obter o último ID inserido

                    using MySqlCommand command = new(sql.ToString(), conn);

                    lastInsertedId = (ulong)await command.ExecuteScalarAsync(); // Executa a consulta e obtém o último ID inserido
                }
                return lastInsertedId;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AdicionarItemAoPedido(int clienteId, int produtoId, int quantidade)
        {
            try
            {
                ulong idPedidoAberto = await RetornaIdPedidoAberto(clienteId);
                ulong pedidoId = 0;
                int estoqueProduto = await RetornaQuantidadeProduto(produtoId);

                int estoqueRestante = estoqueProduto - quantidade;
                AlterarQuantidadeEstoque(produtoId, estoqueRestante);


                if(idPedidoAberto > 0)
                {
                    pedidoId = idPedidoAberto;
                }
                else
                {
                    pedidoId = await AdicionarPedido(clienteId);
                }

                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" INSERT INTO pedidos_itens ");
                    sql.Append(" (`PedidoId`, `ClienteId`, `ProdutoId`, `Quantidade`) ");
                    sql.Append(" VALUES ");
                    sql.AppendFormat(" ('{0}', '{1}', '{2}', '{3}'); ", pedidoId, clienteId, produtoId, quantidade);


                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                }
                return estoqueRestante;
            }
            catch
            {
                throw;
            }
        }


        private async Task<ulong> RetornaIdPedidoAberto(int clienteId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT * FROM pedidos ");
                    sql.Append(" where StatusId = 1 ");
                    sql.AppendFormat(" and ClienteId ={0}; ", clienteId);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    ulong response = 0;

                    if (reader.Read())
                    {
                        response = (ulong)reader.GetInt32(reader.GetOrdinal("PedidoId"));
                    }
                    return response;
                }
            }
            catch
            {
                throw;
            }
        }
        private async Task<int> RetornaQuantidadeProduto(int produtoId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT Quantidade FROM produtos ");
                    sql.AppendFormat(" where ProdutoId = {0} ", produtoId);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    int response = 0;

                    if (reader.Read())
                    {
                        response = reader.GetInt32(reader.GetOrdinal("Quantidade"));
                    }
                    return response;
                }
            }
            catch
            {
                throw;
            }
        }
        private async void AlterarQuantidadeEstoque(int produtoId, int quantidade)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" UPDATE produtos ");
                    sql.AppendFormat(" set quantidade = {0} ", quantidade);
                    sql.AppendFormat(" where ProdutoId = {0} ", produtoId);

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
