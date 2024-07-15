using AplicacionProducto0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AplicacionProducto0.Controllers
{
    [Route("api/Productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProductosContext _context;
        private readonly IConfiguration _configuration;

        public ProductosController(ProductosContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //Modelo para testear la conexión a la base de datos.
        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
                return Ok("Conexión exitosa");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al conectar: {ex.Message}");
            }
        }


        // GET: api/Productos
        [HttpGet("ObtenerProductos")]
        public async Task<ActionResult<IEnumerable<Productos>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Productos>> GetProductos(int id)
        {
            //var resultado = lista.Where(x => x.id == id);
            // var lista 
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto; // lista.FirstorDefault(x => x.id == id);
        }

        [HttpPut("Modificar/{id}")]
        public IActionResult UpdateProducto(int id, [FromBody] Productos updatedProducto)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            producto.Cantidad = updatedProducto.Cantidad;
            _context.SaveChanges();

            return Ok(producto);
        }


        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<Productos>> PostProductos(Productos producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductos", new { id = producto.Id }, producto);
        }

        // DELETE: api/Productos/5
        [HttpDelete("Borrar/{id}")]
        public async Task<IActionResult> DeleteProductos(int id)
        {

            //Productos seleccionar = lista.FirstorDefault( x => x.id ==x);
            // seleccionar = value;
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductosExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}

