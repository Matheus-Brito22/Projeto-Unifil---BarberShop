using BarberShop.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BarberShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        List<Cliente> clientes = new List<Cliente>();

        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            Cliente cliente = clientes.FirstOrDefault(c => c.Id == id);

            if(cliente != null)
            {
                return Ok(cliente);
            }
            return NotFound();
        }

        [HttpGet]
        public IEnumerable<Cliente> FindAll()
        {
            
            return clientes.ToArray();
        }

        [HttpPost]
        public IActionResult Save([FromBody] Cliente cliente) 
        {
            clientes.Add(cliente);
            return Ok(clientes);
        }

        [HttpPut("{id}")]
        public IActionResult update([FromBody] Cliente cliente, int id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            return Ok();
        }
    }
}
