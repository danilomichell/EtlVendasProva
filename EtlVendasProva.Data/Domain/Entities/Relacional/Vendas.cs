using System;
using System.Collections.Generic;

namespace EtlVendasProva.Data.Domain.Entities.Relacional
{
    public partial class Vendas
    {
        public Vendas()
        {
            Itensvenda = new HashSet<Itensvenda>();
        }

        public int Idvenda { get; set; }
        public int Idvendedor { get; set; }
        public int Idcliente { get; set; }
        public DateTime Data { get; set; }
        public decimal Total { get; set; }

        public virtual Clientes IdclienteNavigation { get; set; }
        public virtual Vendedores IdvendedorNavigation { get; set; }
        public virtual ICollection<Itensvenda> Itensvenda { get; set; }
    }
}
