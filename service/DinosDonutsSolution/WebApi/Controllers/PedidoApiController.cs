using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Classes;
using Domain.IRepository;
using InfraData.Repository;
using Microsoft.AspNetCore.Mvc;
using static Domain.Classes.Pedido;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/pedido")]
    public class PedidoApiController : ControllerBase
    {

        private readonly IClienteRepository _clienteRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;


        public PedidoApiController()
        {
            _clienteRepository = new ClienteRepository();
            _pedidoRepository = new PedidoRepository();
            _produtoRepository = new ProdutoRepository();
        }

        [HttpPost]
        public IActionResult Post([FromBody] NovoPedido novoPedido)
        {
            Cliente? cliente = null;
            Produto? produto = null;
            Pedido? pedido = null;


            try
            {
                produto = _produtoRepository.PesquisarPorId(novoPedido.IdProduto);
                produto.VerificarProdutoAtivo();
                produto.VerificarDisponibilidade(novoPedido.Quantidade);
                produto.DeduzirQuantidade(novoPedido.Quantidade);
                _produtoRepository.AlterarQuantidade(produto);

                double valorTotal = Pedido.CalcularValorTotal(novoPedido.Quantidade, produto.Preco);
                int quantidade = novoPedido.Quantidade;
                DateTime dataPedido = DateTime.Now;
                Status status = Status.Andamento;

                cliente = _clienteRepository.PesquisarPorCpf(novoPedido.CpfCliente);

                if (cliente != null)
                {
                    cliente.AlterarPontos(valorTotal);
                    _clienteRepository.AlterarPontosFidelidade(cliente);

                    pedido = new Pedido(cliente, produto, quantidade, valorTotal, status, dataPedido);
                    _pedidoRepository.InserirPedido(pedido);
                }
                else
                {
                    pedido = new Pedido(produto, quantidade, valorTotal, status, dataPedido);
                    _pedidoRepository.InserirPedidoSemCliente(pedido);
                }

                return Ok(new RespostaHttp(200, "Pedido cadastrado com sucesso!"));

            }
            catch (System.Exception ex)
            {
                return BadRequest(new RespostaHttp(400, ex.ToString()));
            }

        }



        [HttpPatch("{id}/status")]
        public IActionResult AlterarStatus(int id, [FromQuery] Status status)
        {
            try
            {
                Pedido pedidoParaEditar = _pedidoRepository.PedidoPorId(id);
                pedidoParaEditar.AlterarStatus(status);
                _pedidoRepository.AlterarStatus(pedidoParaEditar);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RespostaHttp(400, ex.Message));
            }
            return Ok(new RespostaHttp(200, "Status do pedido alterado com sucesso!"));

        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Pedido pedido = null!;
            try
            {
                pedido = _pedidoRepository.PedidoPorId(id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RespostaHttp(400, ex.Message));
            }
            return Ok(pedido);

        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Pedido> listaDePedidos = _pedidoRepository.ListarPedidos();
            if (listaDePedidos.Count == 0)
            {
                return BadRequest(new RespostaHttp(400, "Nenhum pedido cadastrado até o momento"));
            }
            return Ok(listaDePedidos);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Pedido? pedido = null;
            try
            {
                pedido = _pedidoRepository.PedidoPorId(id);
                pedido.ConfereStatusFinalizado();
                _pedidoRepository.ExcluirPedido(pedido.Id);

                Produto produto = pedido.Produto; 
                produto.IncluirQuantidade(pedido.Quantidade);
                _produtoRepository.AlterarQuantidade(produto);
                
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RespostaHttp(400, ex.Message));
            }
            return Ok(new RespostaHttp(200, "Pedido excluído com sucesso!"));

        }
    }


}
