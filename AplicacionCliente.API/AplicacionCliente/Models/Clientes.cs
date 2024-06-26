using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AplicacionCliente.Models
{
    public class Clientes
    {
        [Key]
        [Column("C_ID")]
        public int Id { get; set; }

        [Column("C_Email")]
        public string Email { get; set; }

        [Column("C_RazonSocial")]
        public string RazonSocial { get; set; } 

        [Column("C_Password")]
        public string Password { get; set; }
    }
}
