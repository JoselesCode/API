namespace AplicacionBodega.Models
{
    public class StockProducto
    {
        public int Id { get; set; }    // Identificador único del stock de producto.
        public int ProductoId { get; set; }    // Identificador del producto.
        public int Cantidad { get; set; }    // Cantidad disponible en stock del producto.
    }
}

