using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBodega.Models
{
    public class StockProducto
    {
        [Key]
        [Column("SP_ID")]
        public int Id { get; set; }
        [Column("P_ID")]
        public int ProductoId { get; set; }
        [Column("CANTIDAD")]
        public int CantidadComprada { get; set; }
        [Column("MONTO")]
        public int CostoTotal { get; set; } 

    }
}

