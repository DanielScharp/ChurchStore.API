using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchStore.Domain
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public string? ClienteNome { get; set; }
        public string? ClienteTel { get; set; }
        public int StatusId { get; set; }
        public string? StatusNome { get; set; }
        public DateTime PedidoData { get; set; }
        public double PedidoValor { get; set; }
    }
}
