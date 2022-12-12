using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Classes;

namespace Domain.IRepository
{
    public interface IPedidoRepository
    {
        bool InserirPedido(Pedido pedido);

        List<Pedido> ListarPedidos();

        Pedido PedidoPorId(int id);

        bool AlterarStatus(Pedido pedido);

        bool ExcluirPedido(int id);

        bool InserirPedidoSemCliente(Pedido pedido);
    }
}