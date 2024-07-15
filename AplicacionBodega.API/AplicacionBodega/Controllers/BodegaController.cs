using AplicacionBodega0.DTO;
using AplicacionBodega0.Models;
using AplicacionProducto0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AplicacionBodega0.Controllers
{
    [Route("api/StockProducto")]
    [ApiController]
    public class BodegaController : ControllerBase
    {
        private readonly BodegaContext _context;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public BodegaController(BodegaContext context, IConfiguration configuration, HttpClient httpClient)
        {
            _context = context;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        //Modelo para testear la conexión a la base de datos.
        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            try
            {
                using var connection = new SqlConnection(connectionString);
                {
                    connection.Open();
                    return Ok("Conexión Exitosa.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error connecting: {ex.Message}");
            }
        }

        //Método para controlar el stock solicitado
        [HttpPost("Add")]
        public async Task<IActionResult> RegistrarCompra([FromBody] StockProducto stockProducto)
        {
            // Obtener el producto desde la base de datos
            var productoResponse = await _httpClient.GetAsync($"https://aplicacionproducto.azurewebsites.net/api/Productos/{stockProducto.ProductoId}");
            if (!productoResponse.IsSuccessStatusCode)
            {
                return NotFound("Producto no encontrado.");
            }

            var producto = await productoResponse.Content.ReadFromJsonAsync<Productos>();
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            // Verificar si hay suficiente stock
            if (producto.Cantidad < stockProducto.CantidadComprada)
            {
                return BadRequest("No hay suficiente stock disponible.");
            }

            // Actualizar la cantidad de stock del producto
            producto.Cantidad -= stockProducto.CantidadComprada;
            var updateResponse = await _httpClient.PutAsJsonAsync($"https://aplicacionproducto.azurewebsites.net/api/Productos/Modificar/{producto.Id}", producto);
            if (!updateResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)updateResponse.StatusCode, "Error al actualizar el producto.");
            }

            // Calcular el costo total
            stockProducto.CostoTotal = stockProducto.CantidadComprada * producto.Precio;

            // Registrar la compra en la base de datos de stock
            _context.StockProducto.Add(stockProducto);
            await _context.SaveChangesAsync();

            return Ok(stockProducto);
        }

        [HttpGet("ObtenerCompras")]
        public async Task<ActionResult<IEnumerable<StockProducto>>> GetStockProducto()
        {
            try
            {
                var compras = await _context.StockProducto.ToListAsync();
                return Ok(compras);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

            // GET: api/StockProducto/5
            [HttpGet("{productoId}")]
        public async Task<ActionResult<StockProducto>> GetStockProducto(int productoId)
        {
            var stock = await _context.StockProducto.FirstOrDefaultAsync(s => s.ProductoId == productoId);

            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }

        // PUT: api/StockProducto/5
        [HttpPut("Modificar/{productoId}")]
        public async Task<IActionResult> PutStockProducto(int productoId, [FromBody] StockProductoUpdateDTO UpdateDto)
        {
            if (productoId != UpdateDto.ProductoId)
            {
                return BadRequest("ProductoId en la URL no coincide con el ProductoId en el cuerpo de la solicitud");
            }

            var stock = await _context.StockProducto.FirstOrDefaultAsync(s => s.ProductoId == productoId);

            if (stock == null)
            {
                return NotFound();
            }

            stock.CantidadComprada = UpdateDto.CantidadComprada;

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
        /*
        [HttpPost]
        public async Task<ActionResult<StockProducto>> PostStockProducto(StockProducto stockproducto)
        {
            _context.StockProducto.Add(stockproducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockProducto", new { productoid = stockproducto.ProductoId }, stockproducto);
        }
        */
        private bool StockProductoExists(int productoId)
        {
            return _context.StockProducto.Any(e => e.ProductoId == productoId);
        }
    }

}
