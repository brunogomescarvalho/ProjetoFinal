using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Classes;
using NUnit.Framework;

namespace TestesUnitarios
{
    public class ProdutoTests
    {
        private static Produto _produto = new Produto(1, "Produto", 9.50, DateTime.Now.AddDays(5), true, 100);

        [Test]
        public void Quando_DesativarOuAtivarProduto_Entao_ValorDaPropriedade_Ativo_DeveSerAlterado()
        {
            _produto.DesativarProduto();
            Assert.That(false, Is.EqualTo(_produto.Ativo));
        }

        [Test]
        public void Quando_RealizarVenda_E_QuantidadeDoProdutoSerMenor_Entao_DeveraEnviarExcessao()
        {
            var ex = Assert.Throws<Exception>(() => _produto.VerificarDisponibilidade(150));
            Assert.That(ex!.Message, Is.EqualTo("Quantidade do produto Indisponível"));
        }


        [Test]
        public void Quando_IncluirProduto_E_QuantidadeInformadaIgualMenorQueZero_Entao_DeveraEnviarExcessao()
        {
            var ex = Assert.Throws<Exception>(() => _produto.IncluirQuantidade(0));
            Assert.That(ex!.Message, Is.EqualTo("A quantidade do produto para efetuar o pedido precisa ser maior que zero"));
        }

        [Test]
        public void Quando_Incluir20UnidadesNoEstoque_E_EstoqueJaConter100_Entao_DeveraTotalizar120()
        {
            Produto produto = new Produto(1, "Produto", 9.50, DateTime.Now.AddDays(5), true, 100);
            Assert.AreEqual(120, produto.IncluirQuantidade(20));
        }

        [Test]
        public void Quando_Deduzir_50_Unidades_E_EstoqueConter100_Entao_DeveraTotalizar50()
        {
            Produto produto = new Produto(1, "Produto", 9.50, DateTime.Now.AddDays(5), true, 100);
            _produto.DeduzirQuantidade(50);
            Assert.That(50, Is.EqualTo(_produto.Quantidade));

        }

        [Test]
        public void AoValidarProduto_E_Descricao_EstiverVazio_EntaoDeveraEnviarExcessao()
        {
            Produto _produto = new Produto(0, "", 9.50, DateTime.Now.AddDays(5), true, 100);
            var ex = Assert.Throws<Exception>(() => Produto.ValidarProduto(_produto));
            Assert.That(ex!.Message, Is.EqualTo("A descrição do produto é campo obrigatório para efetuar o cadastro"));
        }

        [Test]
        public void AoValidarProduto_E_Preco_ForMenorIgualZero_EntaoDeveraEnviarExcessao()
        {
            Produto _produto = new Produto(0, "Descricao", 0, DateTime.Now.AddDays(5), true, 100);
            var ex = Assert.Throws<Exception>(() => Produto.ValidarProduto(_produto));
            Assert.That(ex!.Message, Is.EqualTo("O preço do produto não pode ser menor ou igual 0 (zero) "));
        }

        [Test]
        public void AoValidarProduto_E_DataValidade_ForMenorQueHoje_EntaoDeveraEnviarExcessao()
        {
            Produto _produto = new Produto(0, "Descricao", 10, DateTime.Now.AddDays(-5), true, 100);
            var ex = Assert.Throws<Exception>(() => Produto.ValidarProduto(_produto));
            Assert.That(ex!.Message, Is.EqualTo("O produto não pode estar com a data de validade vencida "));
        }


        [Test]
        public void AoValidar_NovoProduto_E_Quantidade_ForMaiorQueZero_EntaoDeveraEnviarExcessao()
        {
            Produto _produto = new Produto(5, "Descricao", 10, DateTime.Now.AddDays(10), true, 100);
            var ex = Assert.Throws<Exception>(() => Produto.ValidarProduto(_produto));
            Assert.That(ex!.Message, Is.EqualTo("O produto só pode ser cadastrado com 0 (zero) unidades"));
        }



    }
}