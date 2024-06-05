using System;
using System.Collections.Generic;
using System.Linq;
using AplicacionProducto.Models;
using Microsoft.EntityFrameworkCore;

namespace AplicacionCliente.Models { 

    public class ClientesContext : DbContext
    {
        public ClientesContext(DbContextOptions<ClientesContext> options) : base(options)
        {
        }

        public DbSet<Productos> Clientes { get; set; } = null!;
    } 
}

