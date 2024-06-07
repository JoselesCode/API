using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AplicacionBodega.Models
{
    public class BodegaContext : DbContext
    {
        public BodegaContext(DbContextOptions<BodegaContext> options) : base(options) { }

        public DbSet<StockProducto> StockProductos { get; set; }
    }
}
