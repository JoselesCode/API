using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AplicacionBodega.Models
{
    public class BodegaContext : DbContext
    {
        public BodegaContext(DbContextOptions<BodegaContext> options) : base(options) { }    // Constructor que inicializa el contexto con las opciones especificadas.

        public DbSet<StockProducto> StockProductos { get; set; }    // DbSet que representa la colección de productos en stock en la DB.
    }
}
