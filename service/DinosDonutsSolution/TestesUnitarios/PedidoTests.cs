using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Classes;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TestesUnitarios
{
    public class PedidoTests
    {


        [Test]
        public void AoCalcularValorTotal_E_PrecoFor5_Quantidade10_EntaoDeveraResultar50()
        {
            int quantidade = 10;
            double preco = 5;
            double valorTotal = Pedido.CalcularValorTotal(quantidade, preco);
            Assert.AreEqual(50, valorTotal);
        }

        [Test]
        public void AoCalcularValorTotal_E__QuantidadeForMenorOuIgualZero_EntaoDeveraEnviarExcessao()
        {
            int quantidade = 0;
            double preco = 5;
            var ex = Assert.Throws<Exception>(() => Pedido.CalcularValorTotal(quantidade, preco));
            Assert.That(ex!.Message, Is.EqualTo("Para efetuar um pedido a quantidade precisa ser maior que zero"));
        }

        [Test]
        public void QuandoAlterarStatusDoPedido_E_PedidoPossuirStatus_DiferenteDeFinalizado_EntaoDeveraAlterarStatus()
        {
            Pedido _pedido = new Pedido(default, null!, null!, default, default, Pedido.Status.Andamento, default);
            _pedido.AlterarStatus(Pedido.Status.Transito);
            Assert.That(Pedido.Status.Transito, Is.EqualTo(_pedido.StatusPedido));

        }

        [Test]
        public void QuandoAlterarStatusDoPedido_E_PedidoPossuirStatus_Finalizado_EntaoDeveraEnviarExcessao()
        {
            Pedido _pedido = new Pedido(default, null!, null!, default, default, Pedido.Status.Finalizado, default);
            var ex = Assert.Throws<Exception>(() => _pedido.AlterarStatus(Pedido.Status.Transito));
            Assert.That(ex!.Message, Is.EqualTo("Esse pedido está finalizado, não é possível editar ou excluir"));

        }
    }
}