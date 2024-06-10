using System;

namespace AplicacionBodega.DTOs
{
	public class StockProductoUpdateDTO
	{
		public int Id { get; set; }
		public int ProductoId { get; set; }
		public int Cantidad { get; set; }
	}
}
