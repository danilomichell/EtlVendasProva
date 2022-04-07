using System;
using System.Collections.Generic;

namespace EtlVendasProva.Data.Domain.Entities.Dw
{
    public partial class DmTempo
    {
        public int IdTempo { get; set; }
        public DateOnly? DataVenda { get; set; }
        public int? NuMes { get; set; }
        public string? NmMes { get; set; }
        public string? SgMes { get; set; }
        public string? Trimestre { get; set; }
    }
}
