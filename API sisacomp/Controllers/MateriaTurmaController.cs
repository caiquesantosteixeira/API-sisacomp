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
    public class MateriaTurmaController : ControllerBase
    {
        private readonly DBContext _context;

        public MateriaTurmaController(DBContext context)
        {
            _context = context;
        }

        // GET: api/MateriaTurma
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriaTurma>>> GetMateriaTurma()
        {
            return await _context.MateriaTurma.ToListAsync();
        }

        // GET: api/MateriaTurma/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaTurma>> GetMateriaTurma(int id)
        {
            var materiaTurma = await _context.MateriaTurma.FindAsync(id);

            if (materiaTurma == null)
            {
                return NotFound();
            }

            return materiaTurma;
        }

        // PUT: api/MateriaTurma/5
        [HttpPut]
        public async Task<IActionResult> PutMateriaTurma(MateriaTurma materiaTurma)
        {
            _context.Entry(materiaTurma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!MateriaTurmaExists(materiaTurma.Id))
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

        // POST: api/MateriaTurma
        [HttpPost]
        public async Task<ActionResult<MateriaTurma>> PostMateriaTurma(MateriaTurma materiaTurma)
        {
            _context.MateriaTurma.Add(materiaTurma);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMateriaTurma", new { id = materiaTurma.Id }, materiaTurma);
        }

        // DELETE: api/MateriaTurma/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MateriaTurma>> DeleteMateriaTurma(int id)
        {
            var materiaTurma = await _context.MateriaTurma.FindAsync(id);
            if (materiaTurma == null)
            {
                return NotFound();
            }

            _context.MateriaTurma.Remove(materiaTurma);
            await _context.SaveChangesAsync();

            return materiaTurma;
        }

        private bool MateriaTurmaExists(int id)
        {
            return _context.MateriaTurma.Any(e => e.Id == id);
        }
    }
}
