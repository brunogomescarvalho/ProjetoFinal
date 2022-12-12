using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Classes;
using Domain.IRepository;
using InfraData.DAO;

namespace InfraData.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {

        private readonly ProdutoDAO _produtoDAO;

        public ProdutoRepository()
        {
            this._produtoDAO = new ProdutoDAO();

        }
        public bool AlterarAtivo(Produto produto)
        {
            if (!_produtoDAO.AlterarAtivo(produto))
            {
                throw new Exception("Algo deu errado ao tentar ativar/desativar o produto");
            }
            return true;

        }

        public bool AlterarQuantidade(Produto produto)
        {
            if (!_produtoDAO.AlterarQuantidade(produto))
            {
                throw new Exception("Algo deu errado ao tentar alterar a quantidade do produto");
            }
            return true;
        }

        public bool CadastrarProduto(Produto produto)
        {
            if (!_produtoDAO.AdicionarProduto(produto))
            {
                throw new Exception("Algo deu errado ao tentar adicionar o produto");
            }
            return true;
        }

        public bool EditarProduto(Produto produto)
        {
            if (!_produtoDAO.EditarProduto(produto))
            {
                throw new Exception("Algo deu errado ao tentar editar o produto");
            }
            return true;
        }

        public List<Produto> ListarProdutos()
        {
            return _produtoDAO.ListarProduto();
        }

        public List<Produto> ListarProdutosAtivos()
        {
            return _produtoDAO.ListarProdutosAtivos();
        }

        public Produto PesquisarPorId(int id)
        {
            return _produtoDAO.PesquisarPorId(id);

        }
    }
}