//Contexto para la gestión de clientes en AplicacionCliente.
using Microsoft.EntityFrameworkCore;

namespace AplicacionCliente.Models;

public class ClientesContext : DbContext
{
    public ClientesContext(DbContextOptions<ClientesContext> options) : base(options) {    //Constructor de la clase que recibe las opciones de configuración del contexto de la DB.
    }

    public DbSet<Clientes> Clientes {get; set;} = null!;    //Propiedad que representa una colección de clientes, que se mapea a la tabla de clientes.
}

