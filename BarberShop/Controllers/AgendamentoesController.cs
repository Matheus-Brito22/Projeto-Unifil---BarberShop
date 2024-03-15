using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarberShop.Data;
using BarberShop.Model;
using BarberShop.Data.DTO.AgendamentoInputModelBarbearia;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using BarberShop.Data.DTO.AgendamentoDTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BarberShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoesController : ControllerBase
    {
        private readonly BarberShopDbContext _context;

        public AgendamentoesController(BarberShopDbContext context)
        {
            _context = context;
        }


        // GET conforme o figma
        [HttpGet("listaAgendamento")]
        public async Task<ActionResult<IEnumerable<AgendamentoGet>>> GetAgendamentos()
        {
            var agendamento = await _context.Agendamentos.ToListAsync();

            if (_context.Agendamentos == null)
            {
                return NotFound();
            }

            List<AgendamentoGet> listaAgendamento = new List<AgendamentoGet>();

            foreach (var item in agendamento)
            {
                TipoServico servico = await _context.TipoServicos.FindAsync(item.TipoServicoId);
                Cliente cliente = await _context.Clientes.FindAsync(item.ClienteId);
                AgendamentoGet agendamentoGet = new AgendamentoGet();
                agendamentoGet.Id = item.Id;
                agendamentoGet.Data = item.Data;
                agendamentoGet.TipoServicoNome = servico.Nome;
                agendamentoGet.ClienteNome = cliente.Nome;
                listaAgendamento.Add(agendamentoGet);
            }

            return listaAgendamento;
        }

        /*GET: api/Agendamentoes
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Agendamento>>> GetAgendamentos()
            {
              if (_context.Agendamentos == null)
              {
                  return NotFound();
              }
                return await _context.Agendamentos.ToListAsync();
        }*/

        // GET: api/Agendamentoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agendamento>> GetAgendamento(int id)
        {
          if (_context.Agendamentos == null)
          {
              return NotFound();
          }
            var agendamento = await _context.Agendamentos.FindAsync(id);

            if (agendamento == null)
            {
                return NotFound();
            }

            return agendamento;
        }

        // PUT: api/Agendamentoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgendamento(int id, AgendamentoPut model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var agendamento = new Agendamento
            {
                Id = model.Id,
                Data = model.Data,
                TipoServicoId = model.TipoServicoId,
                ClienteId = model.ClienteId,
                BarbeariaId = model.BarbeariaId,
                Cliente = await _context.Clientes.FindAsync(model.ClienteId),
                TipoServico = await _context.TipoServicos.FindAsync(model.TipoServicoId),
                Barbearia = await _context.Barbearias.FindAsync(model.BarbeariaId)
            };
            _context.Entry(agendamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgendamentoExists(id))
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

        // POST: api/Agendamentoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        public async Task<ActionResult<Agendamento>> PostAgendamento(Agendamento agendamento)
        {
          if (_context.Agendamentos == null)
          {
              return Problem("Entity set 'BarberShopDbContext.Agendamentos'  is null.");
          }
            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgendamento", new { id = agendamento.Id }, agendamento);
        }*/


        // POST
        [HttpPost("createAgendamento")]
        public async Task<ActionResult> PostAgendamento(AgendamentoPost model)
        {
            if (_context.Agendamentos == null)
            {
                return Problem("Entity set 'BarberShopDbContext.Agendamentos'  is null.");
            }
            var agendamento = new Agendamento
            {
                Data = model.Data,
                TipoServicoId = model.TipoServicoId,
                ClienteId = model.ClienteId,
                BarbeariaId = model.BarbeariaId,
                Cliente = await _context.Clientes.FindAsync(model.ClienteId),
                TipoServico = await _context.TipoServicos.FindAsync(model.TipoServicoId),
                Barbearia = await _context.Barbearias.FindAsync(model.BarbeariaId)
            };
            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAgendamento", new { id = agendamento.Id }, agendamento);
        }

        // DELETE: api/Agendamentoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgendamento(int id)
        {
            if (_context.Agendamentos == null)
            {
                return NotFound();
            }
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }

            _context.Agendamentos.Remove(agendamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AgendamentoExists(int id)
        {
            return (_context.Agendamentos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
