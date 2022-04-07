using System;
using System.Collections.Generic;

namespace EtlVendasProva.Data.Domain.Entities.Relacional
{
    public partial class Vendedores
    {
        public Vendedores()
        {
            Vendas = new HashSet<Vendas>();
        }

        public int Idvendedor { get; set; }
        public string? Nome { get; set; }

        public virtual ICollection<Vendas> Vendas { get; set; }
    }
}
