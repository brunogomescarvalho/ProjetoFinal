using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Domain.Classes;
using static Domain.Classes.Pedido;

namespace InfraData.DAO
{
    public class PedidoDAO
    {
        private const string _connectionString = @"server=.\SQLexpress;initial catalog=DINOS_DONUTS;integrated security=true";

        public PedidoDAO() { }

        public bool InserirPedido(Pedido pedido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string insert = @"INSERT INTO PEDIDO VALUES 
                    (@CLIENTE, @PRODUTO, @QUANTIDADE, @VALORTOTAL, @STATUSPEDIDO, @DATAPEDIDO )";

                    ConverterObjetoParaSql(pedido, comando);
                    comando.CommandText = insert;

                    return comando.ExecuteNonQuery() != 0;
                }

            }
        }

        public bool InserirPedidoSemCliente(Pedido pedido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string insert = @"INSERT INTO PEDIDO 
                    (PRODUTO, QUANTIDADE, VALORTOTAL, STATUSPEDIDO,DATAPEDIDO)
                    VALUES
                    (@PRODUTO, @QUANTIDADE, @VALORTOTAL, @STATUSPEDIDO, @DATAPEDIDO )";

                    ConverterObjetoParaSql(pedido, comando);
                    comando.CommandText = insert;

                    return comando.ExecuteNonQuery() != 0;
                }

            }
        }


        public Pedido? PedidoPorId(int id)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string select = @"SELECT
                    C.ID, C.NOME, C.CPF, C.DATANASCIMENTO, C.PONTOSFIDELIDADE,
                    PD.ID AS IDPRODUTO, PD.DESCRICAO, PD.PRECO, PD.VALIDADE, PD.ATIVO, PD.QUANTIDADE,
                    P.ID AS IDPEDIDO, P.QUANTIDADE AS QDTPEDIDO, P.DATAPEDIDO, P.VALORTOTAL, P.STATUSPEDIDO

                    FROM PEDIDO P  
                    LEFT JOIN CLIENTE C ON (P.CLIENTE = C.ID)
                    INNER JOIN PRODUTO PD ON(PD.ID= P.PRODUTO)

                    WHERE P.ID =@ID";

                    comando.Parameters.AddWithValue("@ID", id);

                    comando.CommandText = select;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        return ConverteSqlObjeto(leitor);
                    }
                }
            }
            return null;

        }

        public List<Pedido> ListarPedidos()
        {
            List<Pedido> listaPedidos = new List<Pedido>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string select = @"SELECT
                    C.ID, C.NOME, C.CPF, C.DATANASCIMENTO, C.PONTOSFIDELIDADE,
                    PD.ID AS IDPRODUTO, PD.DESCRICAO, PD.PRECO, PD.VALIDADE, PD.ATIVO, PD.QUANTIDADE,
                    P.ID AS IDPEDIDO, P.QUANTIDADE AS QDTPEDIDO, P.DATAPEDIDO, P.VALORTOTAL, P.STATUSPEDIDO
                    FROM PEDIDO P  
                    LEFT JOIN CLIENTE C ON (P.CLIENTE = C.ID)
                    INNER JOIN PRODUTO PD ON(PD.ID= P.PRODUTO)";

                    comando.CommandText = select;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        Pedido pedido = ConverteSqlObjeto(leitor);
                        listaPedidos.Add(pedido);
                    }
                }
            }
            return listaPedidos;

        }

        public bool AlterarStatus(Pedido pedido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE PEDIDO SET 
                    STATUSPEDIDO = @STATUSPEDIDO 
                    WHERE ID = @ID";

                    ConverterObjetoParaSql(pedido, comando);
                    comando.CommandText = sql;

                    return comando.ExecuteNonQuery() != 0;

                }
            }

        }

        public bool ExcluirPedido(int id)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"DELETE FROM PEDIDO WHERE ID = @ID";

                    comando.Parameters.AddWithValue("@ID", id);

                    comando.CommandText = sql;
                    return comando.ExecuteNonQuery() != 0;
                }
            }
        }

        private static Pedido ConverteSqlObjeto(SqlDataReader leitor)
        {
            string? idString = leitor["ID"].ToString();
            int id = idString == string.Empty ? default : Convert.ToInt16(leitor["ID"].ToString());

            string? cpf = leitor["CPF"].ToString();
           
            string? nome = (leitor["NOME"]).ToString();

            string? data = leitor["DATANASCIMENTO"].ToString();
            DateTime dataNascimento = data == string.Empty ? default : Convert.ToDateTime(leitor["DATANASCIMENTO"].ToString());

            string? pontosString = leitor["PONTOSFIDELIDADE"].ToString();
            int pontos = pontosString == string.Empty ? default : Convert.ToInt16(leitor["PONTOSFIDELIDADE"].ToString());

            var cliente = new Cliente(id, nome!, cpf!, dataNascimento, pontos);


            int idProduto = Convert.ToInt16(leitor["IDPRODUTO"].ToString());
            string? descricao = (leitor["DESCRICAO"]).ToString();
            double preco = Convert.ToDouble(leitor["PRECO"].ToString());

            DateTime dataValidade = Convert.ToDateTime(leitor["VALIDADE"].ToString()).Date;
            bool ativo = Convert.ToBoolean(leitor["ATIVO"].ToString());
            int quantidade = Convert.ToInt16(leitor["QUANTIDADE"].ToString());

            var produto = new Produto(idProduto, descricao!, preco, dataValidade, ativo, quantidade);



            int idPedido = Convert.ToInt16(leitor["IDPEDIDO"].ToString());
            quantidade = Convert.ToInt16(leitor["QDTPEDIDO"].ToString());
            DateTime dataPedido = Convert.ToDateTime(leitor["DATAPEDIDO"].ToString());
            double valorTotal = Convert.ToDouble(leitor["VALORTOTAL"].ToString());
            Status status = (Status)Convert.ToInt16(leitor["STATUSPEDIDO"].ToString());

            if (cliente.Id == default)
                cliente = null;

            return new Pedido(idPedido, cliente!, produto, quantidade, valorTotal, status, dataPedido);




        }



        private static void ConverterObjetoParaSql(Pedido pedido, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("@ID", pedido.Id);
            if (pedido.Cliente != null)
                comando.Parameters.AddWithValue("@CLIENTE", pedido.Cliente.Id);
            comando.Parameters.AddWithValue("@PRODUTO", pedido.Produto.Id);
            comando.Parameters.AddWithValue("@QUANTIDADE", pedido.Quantidade);
            comando.Parameters.AddWithValue("@VALORTOTAL", pedido.ValorTotal);
            comando.Parameters.AddWithValue("@DATAPEDIDO", pedido.DataPedido.ToString());
            comando.Parameters.AddWithValue("@STATUSPEDIDO", pedido.StatusPedido);
        }
    }
}
