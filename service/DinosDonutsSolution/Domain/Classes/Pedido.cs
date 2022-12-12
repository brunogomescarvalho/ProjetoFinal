using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Classes
{
    public class Pedido
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public double ValorTotal { get; set; }
        public DateTime DataPedido { get; set; }
        public Status StatusPedido { get; set; }
        public Cliente? Cliente { get; set; }
        public Produto Produto { get; set; }

        public Pedido(int id, Cliente cliente, Produto produto, int quantidade, double valorTotal, Status status, DateTime data)
        {

            this.Id = id;
            this.Quantidade = quantidade;
            this.ValorTotal = valorTotal;
            this.DataPedido = data;
            this.StatusPedido = status;
            this.Cliente = cliente;
            this.Produto = produto;
        }

        public Pedido(Cliente cliente, Produto produto, int quantidade, double valorTotal, Status status, DateTime data)
        {
            this.Cliente = cliente;
            this.Produto = produto;
            this.Quantidade = quantidade;
            this.ValorTotal = valorTotal;
            this.DataPedido = data;
            this.StatusPedido = status;

        }

        public Pedido(Produto produto, int quantidade, double valorTotal, Status status, DateTime data)
        {
            this.Produto = produto;
            this.Quantidade = quantidade;
            this.ValorTotal = valorTotal;
            this.DataPedido = data;
            this.StatusPedido = status;
        }


        public static double CalcularValorTotal(int quantidade, double preco)
        {
            if (quantidade <= 0)
            {
                throw new Exception("Para efetuar um pedido a quantidade precisa ser maior que zero");
            }
            return quantidade * preco;
        }

        public void AlterarStatus(Status status)
        {
            bool statusAtivo = ConfereStatusFinalizado();

            if (statusAtivo)
            {
                this.StatusPedido = status;
            }
        }

        public bool ConfereStatusFinalizado()
        {
            if (this.StatusPedido == Status.Finalizado)
            {
                throw new Exception("Esse pedido está finalizado, não é possível editar ou excluir");
            }
            return true;
        }


        public enum Status
        {
            Andamento,
            Transito,
            Finalizado

        }

    }
}