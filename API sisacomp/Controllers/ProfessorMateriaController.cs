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
    public class ProfessorMateriaController : ControllerBase
    {
        private readonly DBContext _context;

        public ProfessorMateriaController(DBContext context)
        {
            _context = context;
        }

        // GET: api/ProfessorMateria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfessorMateria>>> GetProfessorMateria()
        {
            return await _context.ProfessorMateria.ToListAsync();
        }

        // GET: api/ProfessorMateria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfessorMateria>> GetProfessorMateria(int id)
        {
            var professorMateria = await _context.ProfessorMateria.FindAsync(id);

            if (professorMateria == null)
            {
                return NotFound();
            }

            return professorMateria;
        }

        // PUT: api/ProfessorMateria/5
        [HttpPut]
        public async Task<IActionResult> PutProfessorMateria(ProfessorMateria professorMateria)
        {
            
            _context.Entry(professorMateria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessorMateriaExists(professorMateria.Id))
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

        // POST: api/ProfessorMateria
        [HttpPost]
        public async Task<ActionResult<ProfessorMateria>> PostProfessorMateria(ProfessorMateria professorMateria)
        {
            _context.ProfessorMateria.Add(professorMateria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfessorMateria", new { id = professorMateria.Id }, professorMateria);
        }

        // DELETE: api/ProfessorMateria/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProfessorMateria>> DeleteProfessorMateria(int id)
        {
            var professorMateria = await _context.ProfessorMateria.FindAsync(id);
            if (professorMateria == null)
            {
                return NotFound();
            }

            _context.ProfessorMateria.Remove(professorMateria);
            await _context.SaveChangesAsync();

            return professorMateria;
        }

        private bool ProfessorMateriaExists(int id)
        {
            return _context.ProfessorMateria.Any(e => e.Id == id);
        }
    }
}
