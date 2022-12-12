using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Domain.Classes;

namespace InfraData.DAO
{
    public class ClienteDAO
    {
        private const string _connectionString = @"server=.\SQLexpress;initial catalog=DINOS_DONUTS;integrated security=true";

        public ClienteDAO() { }

        public bool AdicionarCliente(Cliente cliente)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string insert = @"INSERT INTO CLIENTE (NOME, CPF, DATANASCIMENTO, PONTOSFIDELIDADE)
                    VALUES (@NOME, @CPF, @DATANASCIMENTO, @PONTOSFIDELIDADE)";

                    ConverterObjetoParaSql(cliente, comando);
                    comando.CommandText = insert;
                    return comando.ExecuteNonQuery() != 0;
                }
            }
        }

        public List<Cliente> ListarClientes()
        {
            List<Cliente> listaClientes = new List<Cliente>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    var select = @"SELECT ID,  NOME, CPF, DATANASCIMENTO, PONTOSFIDELIDADE
                    FROM CLIENTE";

                    comando.CommandText = select;
                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        Cliente cliente = ConverteSqlClienteObjeto(leitor);
                        listaClientes.Add(cliente);
                    }
                }
            }
            return listaClientes;
        }

        public Cliente? PesquisarClienteporCpf(string cpf)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    var select = @"SELECT ID, CPF, NOME, DATANASCIMENTO, PONTOSFIDELIDADE
                    FROM CLIENTE WHERE CPF = @CPF";

                    comando.Parameters.AddWithValue("@CPF", cpf);

                    comando.CommandText = select;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        return ConverteSqlClienteObjeto(leitor);
                    }
                }
            }
            return null;
        }

        public Cliente PesquisarClientePorId(int id)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    var select = @"SELECT ID, CPF, NOME, DATANASCIMENTO, PONTOSFIDELIDADE
                    FROM CLIENTE WHERE ID = @ID";

                    comando.Parameters.AddWithValue("@ID", id);

                    comando.CommandText = select;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        return ConverteSqlClienteObjeto(leitor);
                    }
                }
            }
            return null!;
        }

        public bool EditarCliente(Cliente clienteAlterar)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE CLIENTE SET 
                    NOME = @NOME, 
                    DATANASCIMENTO = @DATANASCIMENTO,
                    CPF = @CPF 
                    WHERE ID = @ID;";

                    ConverterObjetoParaSql(clienteAlterar, comando);

                    comando.CommandText = sql;

                    return comando.ExecuteNonQuery() != 0;

                }
            }
        }

        public bool AlterarPontosFidelidade(Cliente cliente)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE CLIENTE SET 
                    PONTOSFIDELIDADE = @PONTOSFIDELIDADE 
                    WHERE ID = @ID";

                    ConverterObjetoParaSql(cliente, comando);

                    comando.CommandText = sql;

                    return comando.ExecuteNonQuery() != 0;

                }
            }

        }

        public bool VerificarClienteComPedido(int id)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string select = @"SELECT * FROM PEDIDO P 
                    INNER JOIN CLIENTE C ON (P.CLIENTE = C.ID)
                    WHERE CLIENTE = @ID_PESQUISAR";

                    comando.Parameters.AddWithValue("@ID_PESQUISAR", id);
                    comando.CommandText = select;

                    SqlDataReader leitor = comando.ExecuteReader();
                    return leitor.HasRows;
                }
            }
        }

        public bool ExcluirCliente(int id)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @$"DELETE FROM CLIENTE WHERE ID = @ID";

                    comando.Parameters.AddWithValue(@"ID", id);

                    comando.CommandText = sql;
                    return comando.ExecuteNonQuery() != 0;
                }
            }
        }





        private static void ConverterObjetoParaSql(Cliente cliente, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("@ID", cliente.Id);
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
            comando.Parameters.AddWithValue("@DATANASCIMENTO", cliente.DataNascimento.ToString());
            comando.Parameters.AddWithValue("@PONTOSFIDELIDADE", cliente.Pontos);
        }

        private static Cliente ConverteSqlClienteObjeto(SqlDataReader leitor)
        {
            int id = Convert.ToInt16(leitor["ID"]);
            string? cpf = (leitor["CPF"]).ToString();
            string? nome = (leitor["NOME"]).ToString();
            DateTime dataNascimento = Convert.ToDateTime(leitor["DATANASCIMENTO"]).Date;
            int pontos = Convert.ToInt16(leitor["PONTOSFIDELIDADE"]);

            return new Cliente(id, nome!, cpf!, dataNascimento, pontos);

        }
    }
}