using System;
using System.Collections.Generic;

namespace EtlVendasProva.Data.Domain.Entities.Relacional
{
    public partial class Produtos
    {
        public Produtos()
        {
            Itensvenda = new HashSet<Itensvenda>();
        }

        public int Idproduto { get; set; }
        public string? Produto { get; set; }
        public decimal? Preco { get; set; }

        public virtual ICollection<Itensvenda> Itensvenda { get; set; }
    }
}
