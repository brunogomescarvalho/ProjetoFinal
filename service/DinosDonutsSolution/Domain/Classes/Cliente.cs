using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Domain.Classes
{
    public class Cliente
    {

        public int Id { get; set; }
        public string? Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Pontos { get; set; }


        public Cliente(int id, string nome, string cpf, DateTime dataNascimento, int pontos)
        {
            this.Id = id;
            this.Nome = nome;
            this.Cpf = cpf;
            this.DataNascimento = dataNascimento;
            this.Pontos = pontos;
        }

        public int AlterarPontos(double valorCompras)
        {
            return this.Pontos += Convert.ToInt32(valorCompras * 2);
        }

        public static bool ValidarCliente(Cliente cliente)
        {
            if (cliente.Nome!.Length < 3)
            {
                throw new Exception("O nome do cliente precisa ter no mínimo 3 (três) caracteres");
            }
            if (cliente.Cpf.Length < 11 )
            {
                throw new Exception("O cpf do cliente precisa ter 11 (onze) digitos");
            }
            if (cliente.DataNascimento > DateTime.Now)
            {
                throw new Exception("Data de nascimento inválida");
            }
            if (cliente.Id == 0 && cliente.Pontos != 0)
            {
                throw new Exception("Um cliente só pode ser cadastrado com 0 (zero) pontos.");
            }
            return true;
        }
    }
}