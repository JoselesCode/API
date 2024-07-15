using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionProducto0.Models
{
    public class Productos
    {
        [Key]
        [Column("P_ID")]
        public int Id { get; set; }
        [Column("P_Nombre")]
        public string? NombreProducto { get; set; }
        [Column("P_Precio")]
        public int Precio { get; set; }
        [Column("P_Cantidad")]
        public int Cantidad { get; set; }

    }
}