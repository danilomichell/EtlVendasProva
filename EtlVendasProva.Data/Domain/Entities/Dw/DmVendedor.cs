using System;
using System.Collections.Generic;

namespace EtlVendasProva.Data.Domain.Entities.Dw
{
    public partial class DmVendedor
    {
        public DmVendedor()
        {
            FtVendas = new HashSet<FtVendas>();
        }

        public int IdVendedor { get; set; }
        public string? NomeVendedor { get; set; }
        public string? NivelVendedor { get; set; }

        public virtual ICollection<FtVendas> FtVendas { get; set; }
    }
}
