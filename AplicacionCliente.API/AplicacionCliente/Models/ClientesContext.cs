using Microsoft.EntityFrameworkCore;

namespace AplicacionCliente.Models;

public class ClientesContext : DbContext
{
    public ClientesContext(DbContextOptions<ClientesContext> options) : base(options) {
    }

    public DbSet<Clientes> Clientes {get; set;} = null!;
}

