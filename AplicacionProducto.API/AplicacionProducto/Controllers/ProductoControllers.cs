using AplicacionProducto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacionProducto.Controllers
{
    [Route("api/Productos")]
    [ApiController]
    public class ProductoControllers : ControllerBase
    {
        private readonly ProductosContext _context;    //El contexto de la DB.

        public ProductoControllers(ProductosContext context)    //Constructor que inicializa el controlador con el contexto de la DB.
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productos>>> GetProductos()    //Obtiene todos los productos.
        {
            return await _context.Productos.ToListAsync();
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Productos>> GetProductos(int id)    //Obtiene un producto por su ID.
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

        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductos(int id, Productos producto)    //Actualiza un producto existente.
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductosExists(id))
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

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<Productos>> PostProductos(Productos producto)    //Crea un nuevo producto.
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductos", new { id = producto.Id }, producto);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductos(int id)    //Elimina un producto por su ID.
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

        private bool ProductosExists(int id)    //Verifica si un producto existe.
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}

