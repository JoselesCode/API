using Microsoft.AspNetCore.Mvc;
using AplicacionClientes.API.Models;

namespace AplicacionClientes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {

        /*public static List<Clientes> archs = new List<Clientes> {
            new Clientes() {Id= "0", Nombre= "Eduardo"}};

        [HttpGet]

        IList<ClientesController> Get()
        {
         return Cliente;
        }
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }*/
    }
}