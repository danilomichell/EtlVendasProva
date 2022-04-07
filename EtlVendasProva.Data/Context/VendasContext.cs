using Microsoft.EntityFrameworkCore;

namespace EtlVendasProva.Data.Context
{
    public partial class VendasContext : DbContext
    {
        public VendasContext(DbContextOptions<VendasContext> options)
               : base(options)
        {
        }
    }
}
