using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Classes;
using Domain.IRepository;
using InfraData.Repository;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/produto")]
    public class ProdutoApiController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoApiController()
        {
            this._produtoRepository = new ProdutoRepository();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Produto produto)
        {
            try
            {
                Produto novoProduto = new Produto(
                    produto.Id,
                    produto.Descricao,
                    produto.Preco,
                    produto.DataValidade,
                    true,
                    produto.Quantidade);
                    
                Produto.ValidarProduto(novoProduto);
                _produtoRepository.CadastrarProduto(novoProduto);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RespostaHttp(400, ex.Message));
            }
            return Ok(new RespostaHttp(200, "Produto cadastrado com sucesso!"));

        }

        [HttpGet("ativos")]
        public IActionResult GetAtivos()
        {
            List<Produto> produtosDisponiveis = _produtoRepository.ListarProdutosAtivos();

            if (produtosDisponiveis.Count == 0)
            {
                return BadRequest(new RespostaHttp(400, "Nenhum produto disponível no momento"));
            }
            return Ok(produtosDisponiveis);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var listaProdutos = _produtoRepository.ListarProdutos();

            if (listaProdutos.Count == 0)
            {
                return BadRequest(new RespostaHttp(400, "Nenhum produto cadastrado até o momento."));
            }
            return Ok(listaProdutos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Produto produto = _produtoRepository.PesquisarPorId(id);
            if (produto == null)
            {
                return BadRequest(new RespostaHttp(400, "Produto não encontrado"));
            }
            return Ok(produto);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch([FromBody] Produto produto, int id)
        {
            try
            {
                Produto.ValidarProduto(produto);
                Produto produtoParaEditar = _produtoRepository.PesquisarPorId(id);
                produtoParaEditar.Descricao = produto.Descricao;
                produtoParaEditar.Preco = produto.Preco;
                produtoParaEditar.DataValidade = produto.DataValidade;

                _produtoRepository.EditarProduto(produtoParaEditar);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RespostaHttp(400, ex.Message));
            }
            return Ok(new RespostaHttp(200, "Produto editado com sucesso!"));

        }

        [HttpPatch("{id}/ativo")]
        public IActionResult PatchAtivo(int id)
        {
            try
            {
                Produto produtoParaEditar = _produtoRepository.PesquisarPorId(id);
                produtoParaEditar.DesativarProduto();
                _produtoRepository.AlterarAtivo(produtoParaEditar);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RespostaHttp(400, ex.Message));
            }
            return Ok(new RespostaHttp(200, "Produto editado com sucesso!"));

        }

        [HttpPatch("{id}/estoque")]
        public IActionResult PatchQuantidade(int id, [FromQuery] int quantidade)
        {
            try
            {
                Produto produtoParaEditar = _produtoRepository.PesquisarPorId(id);
                produtoParaEditar.IncluirQuantidade(quantidade);
                _produtoRepository.AlterarQuantidade(produtoParaEditar);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RespostaHttp(400, ex.Message));
            }
            return Ok(new RespostaHttp(200, "Produto editado com sucesso!"));

        }




    }
}