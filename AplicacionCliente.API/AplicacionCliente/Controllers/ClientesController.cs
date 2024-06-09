//Las siguientes lineas de codigo son directivas de importacion en c# las cuales permiten acceder a diferentes componentes y funcionalidades dentro del entorno de desarrollo de ASP.NET Core y Entity Framework Core.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AplicacionCliente.Models;

//Controlador de API para manejar las solicitudes relacionadas con los clientes en AplicacionCliente.
namespace AplicacionCliente.Controllers
{
    [Route("api/Clientes")]    //Define la ruta base para las solicitudes dirigidas a este controlador.
    [ApiController]    //Atributo que indica que la clase es un controlador de API y permite que ASP.NET Core realice configuraciones automaticas de comportamiento.
    public class ClientesController : ControllerBase
    {    //Constructor del controlador
        private readonly ClientesContext _context;

        public ClientesController(ClientesContext context)    //Constructor del controlador que recibe un parámetro de tipo ClientesContext, es utilizado para inyectar el contexto de la base de datos en el controlador.
        {
            _context = context;
        }
            
        // GET: api/Clientes
        [HttpGet]    //Atributo que indica que el método responde a las solicitudes HTTP GET.
        public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();    //Retorna lista de clientes obtenida utilizando Entity Framework Core.
        }
            //Metodo de accion en el controlador de clientes maneja las solicitudes para obtener informacion de un cliente especifico por su ID en la API AplicacionCliente.
        // GET: api/Clientes/5
        [HttpGet("{id}")]    //Atributo que indica que el método responde a las solicitudes HTTP GET con una ruta que incluye un parámetro de ID.
        public async Task<ActionResult<Clientes>> GetClientes(int id)
        {
            //var resultado = lista.Where(x => x.id == id);
            // var lista 
            var clientes = await _context.Clientes.FindAsync(id);    //Utiliza el contexto de la base de datos para buscar un cliente con el ID utilizando el método FindAsync.

            if (clientes == null)    //Verifica si el cliente no fue encontrado. Si es así, devuelve un resultado de tipo NotFound.
            {
                return NotFound();
            }

            return clientes; // lista.FirstorDefault(x => x.id == id);    //Retorna el cliente encontrado. En caso contrario, devuelve un resultado exitoso con la información del cliente solicitado.
        }
            //Metodo de accion en el controlador de clientes hace manejo de las solicitudes para actualizar la informacion de un cliente especifico por su id en la API AplicacionCliente.
        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]    //Atributo que indica que el método responde a las solicitudes HTTP PUT con una ruta que incluye un parámetro de ID.
        public async Task<IActionResult> PutClientes(int id, Clientes clientes) 
        {

            if (id != clientes.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientes).State = EntityState.Modified;    //Marca el estado del objeto clientes como modificado para que Entity Framework Core pueda rastrear los cambios en este objeto.

            try
            {
                await _context.SaveChangesAsync();    //Guarda los cambios en la base de datos de manera asincrónica.
            }
            catch (DbUpdateConcurrencyException)    //Captura excepciones de concurrencia al actualizar. verifica si el cliente no existe y devuelve un resultado de "No encontrado", de lo contrario, lanza la excepción.
            {
                if (!ClientesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();    //Retorna un resultado exitoso sin contenido para indicar que la operación de actualización se realizó con éxito.
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes(Clientes clientes)
        {
            _context.Clientes.Add(clientes);    //Agrega el nuevo cliente al conjunto de clientes.
            await _context.SaveChangesAsync();    //guarda los cambios de manera asincrónica.

            return CreatedAtAction("GetClientes", new { id = clientes.Id }, clientes);    //retorna un resultado exitoso con el nuevo cliente creado y la ubicación del recurso creado en el encabezado de respuesta.
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]    //Método responde a las solicitudes HTTP DELETE con una ruta que incluye un parámetro de ID.
        public async Task<IActionResult> DeleteClientes(int id)
        {

            //Cliente seleccionar = lista.FirstorDefault( x => x.id ==x);
            // seleccionar = value;
            var clientes = await _context.Clientes.FindAsync(id);    //Busca el cliente en la base de datos por su ID.
            if (clientes == null)    // Verifica si el cliente no fue encontrado. Si es así, devuelve un resultado de tipo NotFound.
            {
                return NotFound();
            }

            _context.Clientes.Remove(clientes);    //Elimina el cliente.
            await _context.SaveChangesAsync();    //Guarda los cambios de manera asincrónica.

            return NoContent();    //Retorna un resultado exitoso sin contenido para indicar que el cliente fue borrado con éxito.
        }

        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);    //Utiliza LINQ para consultar y determinar si hay algún cliente cuyo ID coincida con el ID proporcionado. Retorna true si existe al menos un cliente con el ID especificado, de lo contrario, retorna false.
        }
    }
}
