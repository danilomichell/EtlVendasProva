using System;
using System.Collections.Generic;

namespace EtlVendasProva.Data.Domain.Entities.Dw
{
    public partial class DmProduto
    {
        public int IdProduto { get; set; }
        public string? NomeProduto { get; set; }
        public decimal? PrecoProduto { get; set; }
        public string? ClasseProduto { get; set; }
    }
}
