using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AplicacionProducto.Models
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
        public int Cantidad { get; set;}

    }
}