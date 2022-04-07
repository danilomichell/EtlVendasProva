using System;
using System.Collections.Generic;

namespace EtlVendasProva.Data.Domain.Entities.Dw
{
    public partial class DmClientes
    {
        public int IdCliente { get; set; }
        public string? NomeCliente { get; set; }
        public string? EstadoCliente { get; set; }
        public char? SexoCliente { get; set; }
        public string? ClasseCliente { get; set; }
    }
}
