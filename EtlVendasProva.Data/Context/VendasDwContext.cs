using Microsoft.EntityFrameworkCore;

namespace EtlVendasProva.Data.Context
{
    public partial class VendasDwContext : DbContext
    {
        public VendasDwContext(DbContextOptions<VendasDwContext> options)
              : base(options)
        {
        }
    }
}
