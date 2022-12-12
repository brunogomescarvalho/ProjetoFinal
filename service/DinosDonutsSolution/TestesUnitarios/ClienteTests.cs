using Domain.Classes;

namespace TestesUnitarios;
public class ClienteTests
{

    [Test]
    public void AoCadastrarCliente_Se_Cpf_ForMenorQue11Digitos_Entao_DeveraTerExcessao()
    {
        var cliente = new Cliente(0, "NOME", "1234567",  DateTime.Now.AddDays(-5), 0);

        var ex = Assert.Throws<Exception>(() => Cliente.ValidarCliente(cliente));
        Assert.That(ex!.Message, Is.EqualTo("O cpf do cliente precisa ter 11 (onze) digitos"));
    }

    [Test]
    public void AoCadastrarCliente_Se_StringNome_ForMenorQue3_Entao_DeveraTerExcessao()
    {
        var cliente = new Cliente(0, "AA", "12345678990",  DateTime.Now.AddDays(-5), 0);

        var ex = Assert.Throws<Exception>(() => Cliente.ValidarCliente(cliente));
        Assert.That(ex!.Message, Is.EqualTo("O nome do cliente precisa ter no mínimo 3 (três) caracteres"));
    }

    [Test]
    public void AoCadastrarCliente_Se_DataDeNascimento_ForMaiorQueHoje_Entao_DeveraTerExcessao()
    {
        var cliente = new Cliente(0, "NOME", "12345678990", DateTime.Now.AddDays(75), 0);

        var ex = Assert.Throws<Exception>(() => Cliente.ValidarCliente(cliente));
        Assert.That(ex!.Message, Is.EqualTo("Data de nascimento inválida"));
    }

    [Test]
    public void AoCadastrarCliente_Se_PontosFidelidade_ForMaiorQueZero_Entao_DeveraTerExcessao()
    {
        var cliente = new Cliente(0, "NOME", "12345678990", DateTime.Now.AddDays(-5), 100);

        var ex = Assert.Throws<Exception>(() => Cliente.ValidarCliente(cliente));
        Assert.That(ex!.Message, Is.EqualTo("Um cliente só pode ser cadastrado com 0 (zero) pontos."));
    }

    [Test]
    public void AoPontuarClienteQueJaTem100Pontos_E_ValorDaCompraFor200_Entao_DeveraPontuar400_E_TotalSera500()
    {
        var cliente = new Cliente(0, "NOME", "12345678990",  DateTime.Now.AddDays(-5), 100);
        cliente.AlterarPontos(200);

        Assert.That(500, Is.EqualTo(cliente.Pontos));

    }

}
