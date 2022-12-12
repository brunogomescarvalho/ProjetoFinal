using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Classes;

namespace Domain.IRepository
{
    public interface IClienteRepository
    {
        bool CadastrarCliente(Cliente cliente);
        bool EditarCliente(Cliente cliente);
        bool ExcluirCliente(int id);
        bool AlterarPontosFidelidade(Cliente cliente);
        List<Cliente> ListarClientes();
        Cliente PesquisarPorId(int id);
        Cliente PesquisarPorCpf(string? cpf);
    }
}