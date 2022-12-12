using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Domain.Classes;

namespace InfraData.DAO
{
    public class ProdutoDAO
    {
        private const string _connectionString = @"server=.\SQLexpress;initial catalog=DINOS_DONUTS;integrated security=true";

        public ProdutoDAO() { }

        public bool AdicionarProduto(Produto produto)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string insert = @$"INSERT INTO PRODUTO (DESCRICAO, PRECO, VALIDADE, ATIVO, QUANTIDADE)
                    VALUES (@DESCRICAO, @PRECO, @VALIDADE, @ATIVO, @QUANTIDADE)";

                    ConverterObjetoParaSql(produto, comando);
                    comando.CommandText = insert;
                    return comando.ExecuteNonQuery() != 0;
                }
            }
        }

        public List<Produto> ListarProduto()
        {
            List<Produto> listaProduto = new List<Produto>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    var select = @"SELECT ID,  
                    DESCRICAO, 
                    PRECO, 
                    VALIDADE, 
                    ATIVO, 
                    QUANTIDADE
                    FROM PRODUTO";

                    comando.CommandText = select;
                    SqlDataReader leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        Produto produto = ConverteSqlObjeto(leitor);
                        listaProduto.Add(produto);
                    }
                }
            }
            return listaProduto;
        }

         public List<Produto> ListarProdutosAtivos()
        {
            List<Produto> listaProduto = new List<Produto>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    var select = @"SELECT ID,  
                    DESCRICAO, 
                    PRECO, 
                    VALIDADE, 
                    ATIVO, 
                    QUANTIDADE
                    FROM PRODUTO
                    WHERE ATIVO = 1";

                    comando.CommandText = select;
                    SqlDataReader leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        Produto produto = ConverteSqlObjeto(leitor);
                        listaProduto.Add(produto);
                    }
                }
            }
            return listaProduto;
        }


        public Produto PesquisarPorId(int id)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    var select = @"SELECT ID,  
                    DESCRICAO, 
                    PRECO, 
                    VALIDADE, 
                    ATIVO, 
                    QUANTIDADE
                    FROM PRODUTO
                    WHERE ID = @ID";

                    comando.Parameters.AddWithValue("@ID", id);

                    comando.CommandText = select;
                    SqlDataReader leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        return ConverteSqlObjeto(leitor);

                    }
                }
            }
            return null!;
        }
        public bool AlterarQuantidade(Produto produto)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE PRODUTO SET 
                    QUANTIDADE = @QUANTIDADE 
                    WHERE ID = @ID";

                    ConverterObjetoParaSql(produto, comando);
                    comando.CommandText = sql;

                    return comando.ExecuteNonQuery() != 0;

                }
            }

        }

        public bool AlterarAtivo(Produto produto)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE PRODUTO SET 
                    ATIVO = @ATIVO 
                    WHERE ID = @ID";

                    ConverterObjetoParaSql(produto, comando);
                    comando.CommandText = sql;

                    return comando.ExecuteNonQuery() != 0;

                }
            }

        }

        public bool EditarProduto(Produto produto)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE PRODUTO SET 
                    DESCRICAO = @DESCRICAO,
                    PRECO = @PRECO, 
                    VALIDADE = @VALIDADE
                    
                    WHERE ID = @ID;";

                    ConverterObjetoParaSql(produto, comando);

                    comando.CommandText = sql;

                    return comando.ExecuteNonQuery() != 0;

                }
            }
        }



        private static void ConverterObjetoParaSql(Produto produto, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("@ID", produto.Id);
            comando.Parameters.AddWithValue("@DESCRICAO", produto.Descricao);
            comando.Parameters.AddWithValue("@PRECO", produto.Preco);
            comando.Parameters.AddWithValue("@VALIDADE", produto.DataValidade.Date);
            comando.Parameters.AddWithValue("@ATIVO", produto.Ativo);
            comando.Parameters.AddWithValue("@QUANTIDADE", produto.Quantidade);
        }


        private static Produto ConverteSqlObjeto(SqlDataReader leitor)
        {
            int id = Convert.ToInt16(leitor["ID"]);
            string? descricao = (leitor["DESCRICAO"]).ToString();
            double preco = Convert.ToDouble(leitor["PRECO"]);

            DateTime dataValidade = Convert.ToDateTime(leitor["VALIDADE"]).Date;
            bool ativo = Convert.ToBoolean(leitor["ATIVO"]);
            int quantidade = Convert.ToInt16(leitor["QUANTIDADE"]);

            return new Produto(id, descricao!, preco, dataValidade, ativo, quantidade);

        }



    }
}