using System;
using System.Collections.Generic;

namespace EtlVendasProva.Data.Domain.Entities.Dw
{
    public partial class DmVendedor
    {
        public int IdVendedor { get; set; }
        public string? NomeVendedor { get; set; }
        public string? NivelVendedor { get; set; }
    }
}
