using System;
using System.Collections.Generic;

namespace EtlVendasProva.Data.Domain.Entities.Relacional
{
    public partial class Itensvenda
    {
        public int Idproduto { get; set; }
        public int Idvenda { get; set; }
        public int Quantidade { get; set; }
        public decimal? Valorunitario { get; set; }
        public decimal? Valortotal { get; set; }
        public decimal? Desconto { get; set; }

        public virtual Produtos IdprodutoNavigation { get; set; } = null!;
        public virtual Vendas IdvendaNavigation { get; set; } = null!;
    }
}
