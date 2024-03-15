using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarberShop.Data;
using BarberShop.Model;
using BarberShop.Data.DTO.BarbeariaInputModel;
using BarberShop.Data.DTO.BarbeariaDTO;

namespace BarberShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarbeariasController : ControllerBase
    {
        private readonly BarberShopDbContext _context;

        public BarbeariasController(BarberShopDbContext context)
        {
            _context = context;
        }

        // GET: api/Barbearias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Barbearia>>> GetLoginBarbeiros()
        {
          if (_context.Barbearias == null)
          {
              return NotFound();
          }
            return await _context.Barbearias.ToListAsync();
        }

        // GET: api/Barbearias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Barbearia>> GetBarbearia(int id)
        {
          if (_context.Barbearias == null)
          {
              return NotFound();
          }
            var barbearia = await _context.Barbearias.FindAsync(id);

            if (barbearia == null)
            {
                return NotFound();
            }

            return barbearia;
        }

        // PUT: api/Barbearias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBarbearia(int id, BarbeariaPut barbeariaPut)
        {
            if (id != barbeariaPut.Id)
            {
                return BadRequest();
            }

            var barbearia = new Barbearia
            {
                Id = barbeariaPut.Id,
                Endereco = barbeariaPut.Endereco,
                Telefone = barbeariaPut.Telefone,
                AdministradorId = barbeariaPut.AdministradorId,
                Administrador = await _context.Administradores.FindAsync(barbeariaPut.AdministradorId)
            };

            _context.Entry(barbearia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarbeariaExists(id))
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

        // POST: api/Barbearias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Barbearia>> PostBarbearia(Barbearia barbearia)
        {
          if (_context.Barbearias == null)
          {
              return Problem("Entity set 'BarberShopDbContext.LoginBarbeiros'  is null.");
          }
            _context.Barbearias.Add(barbearia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBarbearia", new { id = barbearia.Id }, barbearia);
        }
        [HttpPost("createBarbearia")]
        public async Task<ActionResult> PostBarbearia(BarbeariaCreate model)
        {
            if (_context.Barbearias == null)
            {
                return Problem("Entity set 'BarberShopDbContext.LoginBarbeiros'  is null.");
            }
            var barbearia = new Barbearia 
            {
                Endereco = model.Endereco,
                Telefone = model.Telefone,
                AdministradorId = model.AdministradorId,
                Administrador = await _context.Administradores.FindAsync(model.AdministradorId)
            };
            _context.Barbearias.Add(barbearia);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBarbearia", new { id = barbearia.Id }, barbearia);
        }
        // DELETE: api/Barbearias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarbearia(int id)
        {
            if (_context.Barbearias == null)
            {
                return NotFound();
            }
            var barbearia = await _context.Barbearias.FindAsync(id);
            if (barbearia == null)
            {
                return NotFound();
            }

            _context.Barbearias.Remove(barbearia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BarbeariaExists(int id)
        {
            return (_context.Barbearias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
