using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Classes
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public DateTime DataValidade { get; set; }
        public bool Ativo { get; set; }
        public int Quantidade { get; set; }

        public Produto(int id, string descricao, double preco, DateTime dataValidade, bool ativo, int quantidade)
        {
            Id = id;
            Descricao = descricao;
            Preco = preco;
            DataValidade = dataValidade.Date;
            Ativo = ativo;
            Quantidade = quantidade;
        }

        public void DesativarProduto()
        {
            this.Ativo = !this.Ativo;
        }

        public bool VerificarDisponibilidade(int quantidade)
        {
            if (this.Quantidade < quantidade)
            {
                throw new Exception("Quantidade do produto Indisponível");
            }
            return true;
        }

        public bool VerificarProdutoAtivo()
        {
            if (this.Ativo == false)
            {
                throw new Exception("Produto inativo");
            }
            return true;
        }

        public int IncluirQuantidade(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new Exception("A quantidade do produto para efetuar o pedido precisa ser maior que zero");
            }
            return this.Quantidade += quantidade;
        }

        public int DeduzirQuantidade(int quantidade)
        {
            return this.Quantidade -= quantidade;
        }

        public static bool ValidarProduto(Produto produto)
        {
            if (produto.Descricao == string.Empty)
            {
                throw new Exception("A descrição do produto é campo obrigatório para efetuar o cadastro");
            }

            if (produto.Preco <= 0)
            {
                throw new Exception("O preço do produto não pode ser menor ou igual 0 (zero) ");
            }

            if (produto.DataValidade < DateTime.Now)
            {
                throw new Exception("O produto não pode estar com a data de validade vencida ");
            }
            if (produto.Quantidade != 0 && produto.Id != 0)
            {
                throw new Exception("O produto só pode ser cadastrado com 0 (zero) unidades");
            }
            return true;
        }
    }
}