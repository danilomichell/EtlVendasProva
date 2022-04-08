using System;
using System.Collections.Generic;

namespace EtlVendasProva.Data.Domain.Entities.Dw
{
    public partial class DmClientes
    {
        public DmClientes()
        {
            FtVendas = new HashSet<FtVendas>();
        }

        public int IdCliente { get; set; }
        public string? NomeCliente { get; set; }
        public string? EstadoCliente { get; set; }
        public char? SexoCliente { get; set; }
        public string? ClasseCliente { get; set; }

        public virtual ICollection<FtVendas> FtVendas { get; set; }
    }
}
