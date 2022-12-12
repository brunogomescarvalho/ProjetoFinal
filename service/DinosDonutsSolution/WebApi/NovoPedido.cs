using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class NovoPedido
    {
        public string? CpfCliente { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        
    }
}