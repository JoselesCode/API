//Modelo de datos para representar a un cliente en la aplicación AplicacionCliente.
namespace AplicacionCliente.Models
{
    public class Clientes
    {
        public int Id { get; set; }
        public string? RazonSocial { get; set; }
        public string Direccion {get; set; }
        public string? Rut { get; set; }
    }
}
