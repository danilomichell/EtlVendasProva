using System;
using System.Collections.Generic;

namespace EtlVendasProva.Data.Domain.Entities.Dw
{
    public partial class DmProduto
    {
        public DmProduto()
        {
            FtVendas = new HashSet<FtVendas>();
        }

        public int IdProduto { get; set; }
        public string? NomeProduto { get; set; }
        public decimal? PrecoProduto { get; set; }
        public string? ClasseProduto { get; set; }

        public virtual ICollection<FtVendas> FtVendas { get; set; }
    }
}
