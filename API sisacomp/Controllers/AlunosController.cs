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
    public class AlunosController : ControllerBase
    {
        private readonly DBContext _context;

        public AlunosController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Alunos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAluno()
        {
            return await _context.Aluno.ToListAsync();
        }

        // GET: api/Alunos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }
            return aluno;
        }

        [HttpGet]
        [Route("pai")]
        public async Task<ActionResult<List<RetornoAluno>>> GetAlunoByIdPai(int idPai)
        {
            var alunos = await (from aluno in _context.Aluno
                                join Turma in _context.Turma on aluno.IdTurma equals Turma.IdTurma
                                where aluno.IdResponsavel == idPai
                                select new RetornoAluno
                                {
                                    TurmaDoAluno = Turma,
                                    Situacao = "Ok",
                                    IdAluno = aluno.IdAluno,
                                    IdTurma = aluno.IdTurma,
                                    IdResponsavel  = aluno.IdResponsavel,
                                    Nome = aluno.Nome,
                                    Cpf = aluno.Cpf,
                                    DataNascimento = aluno.DataNascimento,
                                    Ativo = aluno.Ativo,
                                    Telefone = aluno.Telefone
                                }).Take(20).OrderByDescending(a => a.IdAluno).ToListAsync();

            if (alunos == null)
            {
                return NotFound();
            }
            return alunos;
        }

        // PUT: api/Alunos/5
        [HttpPut]
        public async Task<IActionResult> PutAluno(Aluno aluno)
        {
            _context.Entry(aluno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(aluno.IdAluno))
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

        // POST: api/Alunos
        [HttpPost]
        public async Task<ActionResult<Retorno>> PostAluno(Aluno aluno)
        {
            var retorno = new Retorno();
            try
            {
                if (_context.Aluno.Any(e => e.Cpf == aluno.Cpf))
                {
                    retorno.Situacao = "Cpf repetido";
                    retorno.Aluno = aluno;
                    retorno.Mensagem = "Cpf repetido";
                    return retorno;
                }

                _context.Aluno.Add(aluno);
                await _context.SaveChangesAsync();

                retorno.Situacao = "Ok";
                retorno.Aluno = aluno;
                retorno.Mensagem = "";

            }
            catch (Exception ex)
            {
                retorno.Situacao = "Erro";
                retorno.Aluno = null;
                retorno.Mensagem = ex.ToString();
            }


            return retorno;
        }

        // DELETE: api/Alunos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Aluno>> DeleteAluno(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            _context.Aluno.Remove(aluno);
            await _context.SaveChangesAsync();

            return aluno;
        }

        private bool AlunoExists(int id)
        {
            return _context.Aluno.Any(e => e.IdAluno == id);
        }

        public class RetornoAluno
        {
            public string Situacao { get; set; }
            public Turma TurmaDoAluno { get; set; }
            public int IdAluno { get; set; }
            public int IdTurma { get; set; }
            public int IdResponsavel { get; set; }
            public string Nome { get; set; }
            public string Cpf { get; set; }
            public DateTime? DataNascimento { get; set; }
            public string Telefone { get; set; }
            public int Ativo { get; set; }
            public string Senha { get; set; }
        }

        public class Retorno
        {
            public string Situacao { get; set; }
            public Aluno Aluno { get; set; }
            public string Mensagem { get; set; }
        }
    }
}
