using Microsoft.EntityFrameworkCore;

namespace AplicacionBodega0.Models
{
    public class BodegaContext : DbContext
    {
        public BodegaContext(DbContextOptions<BodegaContext> options) : base(options) { }

        public DbSet<StockProducto> StockProducto { get; set; }
    }
}
