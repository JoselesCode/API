using AplicacionBodega.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AplicacionBodega.Controllers
{
    [Route("api/StockProducto")]
    [ApiController]
    public class BodegaController : ControllerBase
    {
        private readonly BodegaContext _context;

        public BodegaController(BodegaContext context)
        {
            _context = context;
        }

        // GET: api/StockProducto/5
        [HttpGet("{productoId}")]
        public async Task<ActionResult<StockProducto>> GetStockProducto(int productoId)
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
        public async Task<IActionResult> PutStockProducto(int productoId, [FromBody] int cantidad)
        {
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
                if (!StockProductoExists(productoId))
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
        public async Task<ActionResult<StockProducto>> PostStockProducto(StockProducto stockproducto)
        {
            _context.StockProductos.Add(stockproducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockProducto", new { productoid = stockproducto.ProductoId }, stockproducto);
        }

        private bool StockProductoExists(int productoId)
        {
            return _context.StockProductos.Any(e => e.ProductoId == productoId);
        }
    }

}
