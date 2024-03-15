using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarberShop.Data;
using BarberShop.Model;

namespace BarberShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoServicoesController : ControllerBase
    {
        private readonly BarberShopDbContext _context;

        public TipoServicoesController(BarberShopDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoServicoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoServico>>> GetTipoServico()
        {
          if (_context.TipoServicos == null)
          {
              return NotFound();
          }
            return await _context.TipoServicos.ToListAsync();
        }

        // GET: api/TipoServicoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoServico>> GetTipoServico(int id)
        {
          if (_context.TipoServicos == null)
          {
              return NotFound();
          }
            var tipoServico = await _context.TipoServicos.FindAsync(id);

            if (tipoServico == null)
            {
                return NotFound();
            }

            return tipoServico;
        }

        // PUT: api/TipoServicoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoServico(int id, TipoServico tipoServico)
        {
            if (id != tipoServico.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoServico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoServicoExists(id))
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

        // POST: api/TipoServicoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoServico>> PostTipoServico(TipoServico tipoServico)
        {
          if (_context.TipoServicos == null)
          {
              return Problem("Entity set 'BarberShopDbContext.TipoServico'  is null.");
          }
            _context.TipoServicos.Add(tipoServico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoServico", new { id = tipoServico.Id }, tipoServico);
        }

        // DELETE: api/TipoServicoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoServico(int id)
        {
            if (_context.TipoServicos == null)
            {
                return NotFound();
            }
            var tipoServico = await _context.TipoServicos.FindAsync(id);
            if (tipoServico == null)
            {
                return NotFound();
            }

            _context.TipoServicos.Remove(tipoServico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoServicoExists(int id)
        {
            return (_context.TipoServicos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
