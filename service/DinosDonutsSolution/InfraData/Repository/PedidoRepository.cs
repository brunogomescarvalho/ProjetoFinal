using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Classes;
using Domain.IRepository;
using InfraData.DAO;

namespace InfraData.Repository
{
    public class PedidoRepository : IPedidoRepository
    {

        private readonly PedidoDAO _pedidoDAO;

        public PedidoRepository()
        {
            this._pedidoDAO = new PedidoDAO();

        }
        public bool InserirPedido(Pedido pedido)
        {
            if (!_pedidoDAO.InserirPedido(pedido))
            {
                throw new Exception("Algo deu errado ao tentar cadastrar o pedido.");
            }
            return true;
        }

        public bool InserirPedidoSemCliente(Pedido pedido)
        {
            if (!_pedidoDAO.InserirPedidoSemCliente(pedido))
            {
                throw new Exception("Algo deu errado ao tentar cadastrar o pedido.");
            }
            return true;
        }

        public List<Pedido> ListarPedidos()
        {
            return _pedidoDAO.ListarPedidos();
        }

        public Pedido PedidoPorId(int id)
        {
            Pedido pedido = _pedidoDAO.PedidoPorId(id)!;
            if (pedido == null)
            {
                throw new Exception("Pedido não localizado");
            }
            return pedido;
        }

        public bool AlterarStatus(Pedido pedido)
        {
            if (!this._pedidoDAO.AlterarStatus(pedido))
            {
                throw new Exception("Algo deu errado ao tentar alterar o status do pedido");
            }
            return true;
        }

        public bool ExcluirPedido(int id)
        {
            if (!_pedidoDAO.ExcluirPedido(id))
            {
                throw new Exception("Algo deu errado ao tentar excluir o pedido");
            }

            return true;
        }
    }
}