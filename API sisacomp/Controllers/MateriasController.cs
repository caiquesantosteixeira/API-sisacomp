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
    public class MateriasController : ControllerBase
    {
        private readonly DBContext _context;

        public MateriasController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Materias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materia>>> GetMateria()
        {
            return await _context.Materia.ToListAsync();
        }

        // GET: api/Materias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Materia>> GetMateria(int id)
        {
            var materia = await _context.Materia.FindAsync(id);

            if (materia == null)
            {
                return NotFound();
            }

            return materia;
        }

        [HttpGet]
        [Route("turma")]
        public async Task<ActionResult<List<Materia>>> GetMateriaByTurma(int id)
        {
            var materia = await (from mat in _context.Materia
                                 join turmaMateria in _context.MateriaTurma on mat.IdMateria equals turmaMateria.IdMateria
                                 where turmaMateria.IdTurma == id
                                 select new Materia
                                 {
                                     IdMateria = mat.IdMateria,
                                     Nome = mat.Nome,
                                     IdMateriaTurma = turmaMateria.Id,
                                     IdProfessorMateria = _context.ProfessorMateria.FirstOrDefault(a => a.IdMateria == mat.IdMateria)
                                 }).OrderByDescending(a => a.IdMateria).ToListAsync();

            if (materia == null)
            {
                return NotFound();
            }

            return materia;
        }

        // PUT: api/Materias/5
        [HttpPut]
        public async Task<IActionResult> PutMateria( Materia materia)
        {
            

            _context.Entry(materia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MateriaExists(materia.IdMateria))
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

        // POST: api/Materias
        [HttpPost]
        public async Task<ActionResult<Materia>> PostMateria(Materia materia)
        {
            _context.Materia.Add(materia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMateria", new { id = materia.IdMateria }, materia);
        }

        // DELETE: api/Materias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Materia>> DeleteMateria(int id)
        {
            var materia = await _context.Materia.FindAsync(id);
            if (materia == null)
            {
                return NotFound();
            }

            _context.Materia.Remove(materia);
            await _context.SaveChangesAsync();

            return materia;
        }

        private bool MateriaExists(int id)
        {
            return _context.Materia.Any(e => e.IdMateria == id);
        }
    }
}
