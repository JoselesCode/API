using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionBodega0.DTO
{
    public class StockProductoUpdateDTO
    {
        [Key]
        [Column("SP_ID")]
        public int Id { get; set; }
        [Column("C_ID")]
        public int ClienteId { get; set; }
        [Column("P_ID")]
        public int ProductoId { get; set; }
        [Column("CANTIDAD")]
        public int CantidadComprada { get; set; }
        [Column("MONTO")]
        public int CostoTotal { get; set; }

    }
}
