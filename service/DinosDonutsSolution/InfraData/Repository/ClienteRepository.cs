using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Classes;
using Domain.IRepository;
using InfraData.DAO;

namespace InfraData.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteDAO _clienteDAO;

        public ClienteRepository()
        {
            this._clienteDAO = new ClienteDAO();
        }

        public bool AlterarPontosFidelidade(Cliente cliente)
        {
            if (!_clienteDAO.AlterarPontosFidelidade(cliente))
            {
                throw new Exception("Algo deu errado ao tentar editar os pontos fidelidade");
            }
            return true;

        }

        public bool CadastrarCliente(Cliente cliente)
        {
            if (_clienteDAO.PesquisarClienteporCpf(cliente.Cpf) != null)
            {
                throw new Exception("Algo deu errado! CPF já Cadastrado");
            }

            if (!_clienteDAO.AdicionarCliente(cliente))
            {
                throw new Exception("Algo deu errado ao tentar adicionar o cliente");
            }
            return true;

        }

        public Cliente PesquisarPorCpf(string? cpf)
        {
            return _clienteDAO.PesquisarClienteporCpf(cpf!)!;
        }

        public bool EditarCliente(Cliente cliente)
        {
            if (!_clienteDAO.EditarCliente(cliente))
            {
                throw new Exception("Algo deu errado ao tentar editar o cliente");
            }
            return true;
        }

        public bool ExcluirCliente(int id)
        {
            if (_clienteDAO.VerificarClienteComPedido(id))
            {
                throw new Exception("Não é possivel excluir cliente com pedido cadastrado");
            }
            if (!_clienteDAO.ExcluirCliente(id))
            {
                throw new Exception("Algo deu errado ao tentar excluir o cliente");
            }
            return true;
        }

        public List<Cliente> ListarClientes()
        {
            return _clienteDAO.ListarClientes();
        }

        public Cliente PesquisarPorId(int id)
        {
            return _clienteDAO.PesquisarClientePorId(id)!;
        }
    }
}