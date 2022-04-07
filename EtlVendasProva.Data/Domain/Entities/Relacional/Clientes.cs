using System;
using System.Collections.Generic;

namespace EtlVendasProva.Data.Domain.Entities.Relacional
{
    public partial class Clientes
    {
        public Clientes()
        {
            Vendas = new HashSet<Vendas>();
        }

        public int Idcliente { get; set; }
        public string? Cliente { get; set; }
        public string? Estado { get; set; }
        public char? Sexo { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Vendas> Vendas { get; set; }
    }
}
