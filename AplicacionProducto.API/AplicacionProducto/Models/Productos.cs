using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacionProducto.Models
{
    public class Productos
    {
        public int Id { get; set; }    //Identificador único del producto.
        public string? NombreProducto { get; set; }    //Nombre del producto, puede ser null.
        public int Precio { get; set; }    //Precio del producto

    }
}
