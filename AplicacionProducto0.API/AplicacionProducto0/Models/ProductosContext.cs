using AplicacionProducto0.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AplicacionProducto0.Models
{

    public class ProductosContext : DbContext
    {
        public ProductosContext(DbContextOptions<ProductosContext> options) : base(options)
        {
        }

        public DbSet<Productos> Productos { get; set; } = null!;
    }
}

