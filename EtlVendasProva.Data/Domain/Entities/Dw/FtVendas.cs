using System;
using System.Collections.Generic;

namespace EtlVendasProva.Data.Domain.Entities.Dw
{
    public partial class FtVendas
    {
        public int IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public int IdTempo { get; set; }
        public int IdProduto { get; set; }
        public int? QtdVendasRealizadas { get; set; }
        public decimal? ValUnitarioProduto { get; set; }
        public decimal? ValTotalVenda { get; set; }
        public decimal? DescontoTotal { get; set; }

        public virtual DmClientes IdClienteNavigation { get; set; } = null!;
        public virtual DmProduto IdProdutoNavigation { get; set; } = null!;
        public virtual DmTempo IdTempoNavigation { get; set; } = null!;
        public virtual DmVendedor IdVendedorNavigation { get; set; } = null!;
    }
}
