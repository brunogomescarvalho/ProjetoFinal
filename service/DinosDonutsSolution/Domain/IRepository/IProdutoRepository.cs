using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Classes;

namespace Domain.IRepository
{
    public interface IProdutoRepository
    {
        bool CadastrarProduto(Produto produto);
        bool EditarProduto(Produto produto);
        bool AlterarQuantidade(Produto produto);
        bool AlterarAtivo(Produto produto);
        List<Produto> ListarProdutos();
         List<Produto> ListarProdutosAtivos();
        Produto PesquisarPorId(int id);

    }
}