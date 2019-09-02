using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_sisacomp.Models;

namespace API_sisacomp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly DBContext _context;

        public AgendaController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Agenda
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agenda>>> GetAgenda()
        {
            return await _context.Agenda.ToListAsync();
        }

        // GET: api/Agenda/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agenda>> GetAgenda(int id)
        {
            var agenda = await _context.Agenda.FindAsync(id);

            if (agenda == null)
            {
                return NotFound();
            }

            return agenda;
        }

        [HttpGet("{id}")]
        [Route("getbyturma")]
        public async Task<ActionResult<List<Agenda>>> GetReclamacaoByIdTurma(int idTurma)
        {
            var agendas = await (from agenda in _context.Agenda
                                     select new Agenda
                                     {
                                         IdAgenda = agenda.IdAgenda,
                                         IdTurma = agenda.IdAgenda,
                                         Titulo = agenda.Titulo,
                                         Texto = agenda.Texto,
                                         Data = agenda.Data
                                     }).Take(20).OrderByDescending(a => a.IdTurma).ToListAsync();

            if (agendas == null)
            {
                return NotFound();
            }
            return agendas;
        }

        // PUT: api/Agenda/5
        [HttpPut]
        public async Task<IActionResult> PutAgenda(Agenda agenda)
        {
            agenda.Data = DateTime.Now;

            _context.Entry(agenda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgendaExists(agenda.IdAgenda))
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

        // POST: api/Agenda
        [HttpPost]
        public async Task<ActionResult<Agenda>> PostAgenda(Agenda agenda)
        {
            agenda.Data = DateTime.Now;
            _context.Agenda.Add(agenda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgenda", new { id = agenda.IdAgenda }, agenda);
        }

        // DELETE: api/Agenda/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Agenda>> DeleteAgenda(int id)
        {
            var agenda = await _context.Agenda.FindAsync(id);
            if (agenda == null)
            {
                return NotFound();
            }

            _context.Agenda.Remove(agenda);
            await _context.SaveChangesAsync();

            return agenda;
        }

        private bool AgendaExists(int id)
        {
            return _context.Agenda.Any(e => e.IdAgenda == id);
        }
    }
}
