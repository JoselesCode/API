using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AplicacionProducto.Models
{

    public class ProductosContext : DbContext
    {
        public ProductosContext(DbContextOptions<ProductosContext> options) : base(options)    // Constructor que inicializa el contexto con las opciones especificadas.
        {
        }

        public DbSet<Productos> Productos { get; set; } = null!;    // DbSet que representa la colección de productos en la DB.
    }
}

