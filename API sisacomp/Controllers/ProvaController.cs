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
    public class ProvaController : ControllerBase
    {
        private readonly DBContext _context;

        public ProvaController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Prova
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prova>>> GetProva()
        {
            return await _context.Prova.ToListAsync();
        }

        [HttpGet]
        [Route("provaByMateria")]
        public async Task<ActionResult<List<Prova>>> GetProvaByMaterua(int id)
        {
            try
            {

                return await _context.Prova.Where(a =>a.IdMateria == id).ToListAsync();

            }
            catch (Exception ex)
            {

            }
            return null;
        }

        // GET: api/Prova/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prova>> GetProva(int id)
        {
            var prova = await _context.Prova.FindAsync(id);

            if (prova == null)
            {
                return NotFound();
            }

            return prova;
        }

        // PUT: api/Prova/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProva(int id, Prova prova)
        {
            if (id != prova.IdProva)
            {
                return BadRequest();
            }

            _context.Entry(prova).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvaExists(id))
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

        // POST: api/Prova
        [HttpPost]
        public async Task<ActionResult<Prova>> PostProva(Prova prova)
        {
            _context.Prova.Add(prova);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProva", new { id = prova.IdProva }, prova);
        }

        // DELETE: api/Prova/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Prova>> DeleteProva(int id)
        {
            var prova = await _context.Prova.FindAsync(id);
            if (prova == null)
            {
                return NotFound();
            }

            _context.Prova.Remove(prova);
            await _context.SaveChangesAsync();

            return prova;
        }

        private bool ProvaExists(int id)
        {
            return _context.Prova.Any(e => e.IdProva == id);
        }
    }
}
