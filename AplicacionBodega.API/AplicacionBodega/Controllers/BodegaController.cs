using AplicacionBodega.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AplicacionBodega.Controllers
{
    [Route("api/StockProducto")]
    [ApiController]
    public class BodegaController : ControllerBase
    {
        private readonly BodegaContext _context;    // El contexto de la base de datos
        
        public BodegaController(BodegaContext context)    //Constructor que inicializa el controlador con el contexto de la DB.
        {
            _context = context;
        }

        // GET: api/StockProducto/5
        [HttpGet("{productoId}")]    
        public async Task<ActionResult<StockProducto>> GetStockProducto(int productoId)    //Obtiene un producto en stock por su ID.
        {
            var stock = await _context.StockProductos.FirstOrDefaultAsync(s => s.ProductoId == productoId);

            if (stock == null)
            {
                return NotFound();
            }

            return stock;    
        }

        // PUT: api/StockProducto/5
        [HttpPut("{productoId}")]
        public async Task<IActionResult> PutStockProducto(int productoId, [FromBody] int cantidad)    // Actualiza la cantidad de un producto en stock existente.
        {
            if (productoId != updateDto.ProductoId)    
            {
                return BadRequest("ProductoId en la URL no coincide con el ProductoId en el cuerpo de la solicitud");
            }

            var stock = await _context.StockProductos.FirstOrDefaultAsync(s => s.ProductoId == productoId);

            if (stock == null)
            {
                return NotFound();
            }

            stock.Cantidad = cantidad;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockProductoExists(productoId))    // Verifica si un producto en stock existe
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StockProducto
        [HttpPost]
        public async Task<ActionResult<StockProducto>> PostStockProducto(StockProducto stockproducto)    // Crea un nuevo producto en stock.
        {
            _context.StockProductos.Add(stockproducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockProducto", new { productoid = stockproducto.ProductoId }, stockproducto);
        }

        private bool StockProductoExists(int productoId)    // Verifica si un producto en stock existe.
        {
            return _context.StockProductos.Any(e => e.ProductoId == productoId);
        }
    }

}
