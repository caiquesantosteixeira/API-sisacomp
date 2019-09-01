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
    public class ReclamacaoController : ControllerBase
    {
        private readonly DBContext _context;

        public ReclamacaoController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Reclamacao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reclamacao>>> GetReclamacao()
        {
            return await _context.Reclamacao.ToListAsync();
        }

        // GET: api/Reclamacao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reclamacao>> GetReclamacao(int id)
        {
            var reclamacao = await _context.Reclamacao.FindAsync(id);

            if (reclamacao == null)
            {
                return NotFound();
            }

            return reclamacao;
        }

        [HttpGet("{id}")]
        [Route("getbyaluno")]
        public async Task<ActionResult<List<Reclamacao>>> GetReclamacaoByIdAluno(int idAluno)
        {
            var reclamacoes = await (from reclamacao in _context.Reclamacao
                                select new Reclamacao
                                {
                                   IdReclamacao = reclamacao.IdReclamacao,
                                   IdAluno = reclamacao.IdAluno,
                                   Titulo =  reclamacao.Titulo,
                                   Texto = reclamacao.Texto

                                }).Take(20).OrderByDescending(a => a.IdAluno).ToListAsync();

            if (reclamacoes == null)
            {
                return NotFound();
            }
            return reclamacoes;
        }


        // PUT: api/Reclamacao/5
        [HttpPut]
        public async Task<IActionResult> PutReclamacao( Reclamacao reclamacao)
        {
           

            _context.Entry(reclamacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReclamacaoExists(reclamacao.IdReclamacao))
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

        // POST: api/Reclamacao
        [HttpPost]
        public async Task<ActionResult<Reclamacao>> PostReclamacao(Reclamacao reclamacao)
        {
            try
            {
                _context.Reclamacao.Add(reclamacao);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetReclamacao", new { id = reclamacao.IdReclamacao }, reclamacao);
            }
            catch(Exception ex)
            {
                return CreatedAtAction("GetReclamacao", new { id = reclamacao.IdReclamacao }, reclamacao);
            }
        }

        // DELETE: api/Reclamacao/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reclamacao>> DeleteReclamacao(int id)
        {
            var reclamacao = await _context.Reclamacao.FindAsync(id);
            if (reclamacao == null)
            {
                return NotFound();
            }

            _context.Reclamacao.Remove(reclamacao);
            await _context.SaveChangesAsync();

            return reclamacao;
        }

        private bool ReclamacaoExists(int id)
        {
            return _context.Reclamacao.Any(e => e.IdReclamacao == id);
        }
    }
}
