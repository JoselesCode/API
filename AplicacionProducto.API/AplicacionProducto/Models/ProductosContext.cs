using Microsoft.EntityFrameworkCore;

namespace AplicacionProducto.Models
{

    public class ProductosContext : DbContext
    {
        public ProductosContext(DbContextOptions<ProductosContext> options) : base(options)
        {
        }

        public DbSet<Productos> Productos { get; set; } = null!;
    }
}

