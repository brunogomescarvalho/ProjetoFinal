using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Classes;
using Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using InfraData.Repository;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteApiController : ControllerBase
    {

        private readonly IClienteRepository _clienteRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public ClienteApiController()
        {
            _clienteRepository = new ClienteRepository();
            _pedidoRepository = new PedidoRepository();
        }
        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            try
            {
                Cliente.ValidarCliente(cliente);
                _clienteRepository.CadastrarCliente(cliente);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RespostaHttp(400, ex.Message));
            }
            return Ok(new RespostaHttp(200, "Cliente cadastrado com sucesso!"));

        }

        [HttpGet]
        public IActionResult Get()
        {
            var listaClientes = _clienteRepository.ListarClientes();

            if (listaClientes.Count == 0)
            {
                return BadRequest(new RespostaHttp(400, "Nenhum cliente cadastrado até o momento."));
            }

            return Ok(listaClientes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            return Ok(_clienteRepository.PesquisarPorId(id));

        }


        [HttpPatch("{id}")]
        public IActionResult Patch([FromBody] Cliente cliente, int id)
        {
            try
            {
                Cliente.ValidarCliente(cliente);
                Cliente clienteParaEditar = _clienteRepository.PesquisarPorId(id);
                clienteParaEditar.Nome = cliente.Nome;
                clienteParaEditar.Cpf = cliente.Cpf;
                clienteParaEditar.DataNascimento = cliente.DataNascimento;

                _clienteRepository.EditarCliente(clienteParaEditar);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RespostaHttp(400, ex.Message));
            }
            return Ok(new RespostaHttp(200, "Cliente editado com sucesso!"));

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _clienteRepository.ExcluirCliente(id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RespostaHttp(400, ex.Message));
            }

            return Ok(new RespostaHttp(200, "Cliente removido com sucesso."));
        }

    }
}