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
    public class ResponsavelController : ControllerBase
    {
        private readonly DBContext _context;

        public ResponsavelController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Responsavels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Responsavel>>> GetResponsavel()
        {
            return await _context.Responsavel.ToListAsync();
        }

        // GET: api/Responsavels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Responsavel>>> GetResponsavel(int id)
        {
            var responsavel = await (from resp in _context.Responsavel where resp.IdResponsavel > id
                                     select new Responsavel{
                                         IdResponsavel = resp.IdResponsavel,
                                         Nome = resp.Nome,
                                         Cpf = resp.Cpf,
                                         Endereco = resp.Endereco,
                                         Telefone = resp.Telefone,
                                         Ativo = resp.Ativo,
                                         Senha = resp.Senha,


                                     }).Take(20).OrderByDescending(a => a.IdResponsavel) .ToListAsync();


            if (responsavel == null)
            {
                return NotFound();
            }

            return responsavel;
        }

        [HttpGet]
        [Route("nome")]
        public async Task<ActionResult<List<Responsavel>>> GetResponsavelNome(string nome)
        {
            var responsavel = await (from resp in _context.Responsavel
                                     where resp.Nome.Contains(nome)
                                     select new Responsavel
                                     {
                                         IdResponsavel = resp.IdResponsavel,
                                         Nome = resp.Nome,
                                         Cpf = resp.Cpf,
                                         Endereco = resp.Endereco,
                                         Telefone = resp.Telefone,
                                         Ativo = resp.Ativo,
                                         Senha = resp.Senha,


                                     }).Take(20).OrderByDescending(a => a.IdResponsavel).ToListAsync();


            if (responsavel == null)
            {
                return NotFound();
            }

            return responsavel;
        }

        [HttpGet]
        [Route("cpf")]
        public async Task<ActionResult<List<Responsavel>>> GetResponsavelCpf(string cpf)
        {
            var responsavel = await (from resp in _context.Responsavel
                                     where resp.Cpf.Contains(cpf)
                                     select new Responsavel
                                     {
                                         IdResponsavel = resp.IdResponsavel,
                                         Nome = resp.Nome,
                                         Cpf = resp.Cpf,
                                         Endereco = resp.Endereco,
                                         Telefone = resp.Telefone,
                                         Ativo = resp.Ativo,
                                         Senha = resp.Senha,


                                     }).Take(20).OrderByDescending(a => a.IdResponsavel).ToListAsync();


            if (responsavel == null)
            {
                return NotFound();
            }

            return responsavel;
        }

        [HttpGet]
        [Route("Login")]
        public async Task<ActionResult<Responsavel>> GetResponsavel(string login, string senha)
        {
            try
            {
                var resps = await (from resp in _context.Responsavel
                                   where resp.Cpf == login && resp.Senha == senha
                                   select new Responsavel
                                   {
                                       IdResponsavel = resp.IdResponsavel,
                                       Nome = resp.Nome,
                                       Cpf = resp.Cpf,
                                       Senha = resp.Senha,

                                   }).Take(20).FirstOrDefaultAsync();
                if (resps != null)
                {
                    resps.Filho = _context.Aluno.Where(a => a.IdResponsavel == resps.IdResponsavel).ToList();
                }

                return resps;
            }
            catch (Exception ex) {

            }
            return null;
        }

        // PUT: api/Responsavels/5
        [HttpPut]
        public async Task<ActionResult<Retorno>> PutResponsavel(Responsavel responsavel)
        {
            var retorno = new Retorno();

            _context.Entry(responsavel).State = EntityState.Modified;

            try
            {
                
                await _context.SaveChangesAsync();

                retorno.Situacao = "Ok";
                retorno.Responsavel = responsavel;
                retorno.Mensagem = "";
            }
            catch (Exception ex)
            {
                retorno.Situacao = "Erro";
                retorno.Responsavel = null;
                retorno.Mensagem = ex.ToString();
            }

            return retorno;
        }

        // POST: api/Responsavels
        [HttpPost]
        public async Task<ActionResult<Retorno>> PostResponsavel(Responsavel responsavel)
        {
            var retorno = new Retorno();
            try
            {
                if (_context.Responsavel.Any(e => e.Cpf == responsavel.Cpf))
                {
                    retorno.Situacao = "Cpf repetido";
                    retorno.Responsavel = responsavel;
                    retorno.Mensagem = "Cpf repetido";
                    return retorno;
                }

                _context.Responsavel.Add(responsavel);
                await _context.SaveChangesAsync();

                retorno.Situacao = "Ok";
                retorno.Responsavel = responsavel;
                retorno.Mensagem = "";
                
            }
            catch (Exception ex)
            {
                retorno.Situacao = "Erro";
                retorno.Responsavel = null;
                retorno.Mensagem = ex.ToString();
            }


            return retorno;
        }

        // DELETE: api/Responsavels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Responsavel>> DeleteResponsavel(int id)
        {
            var responsavel = await _context.Responsavel.FindAsync(id);
            if (responsavel == null)
            {
                return NotFound();
            }

            _context.Responsavel.Remove(responsavel);
            await _context.SaveChangesAsync();

            return responsavel;
        }

        private bool ResponsavelExists(int id)
        {
            return _context.Responsavel.Any(e => e.IdResponsavel == id);
        }

        public class Retorno{
            public string Situacao { get; set; }
            public Responsavel Responsavel { get; set; }
            public string Mensagem { get; set; }
        }
    }
}
