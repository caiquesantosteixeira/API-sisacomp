﻿  using System;
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
    public class ProfessorController : ControllerBase
    {
        private readonly DBContext _context;

        public ProfessorController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Professor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professor>>> GetProfessor()
        {
            try
            {
                var professores = await (from prof in _context.Professor
                                       select new Professor
                                       {
                                           IdProfessor = prof.IdProfessor,
                                           Nome = prof.Nome,
                                           Cpf = prof.Cpf,
                                           Telefone = prof.Telefone
                                       }).OrderByDescending(a => a.IdProfessor).ToListAsync();
                return professores;
            }
            catch (Exception ex) {

            }
            return null;
        }

        // GET: api/Professor/5
        [HttpGet]
        [Route("GetProfessor")]
        public async Task<ActionResult<List<Professor>>> GetProfessor(int idMateria)
        {
            var professor = await (from prof in _context.Professor
                                   where prof.IdMateria == idMateria
                                   select new Professor
                                   {
                                       IdProfessor = prof.IdProfessor,
                                       Nome = prof.Nome,
                                       Cpf = prof.Cpf,
                                       Telefone = prof.Telefone
                                   }).OrderByDescending(a => a.IdProfessor).ToListAsync();

            
            return professor;
        }

        [HttpGet]
        [Route("Login")]
        public async Task<ActionResult<List<Professor>>> GetProfessor(string login, string senha)
        {
            try
            {
                var professor = await (from prof in _context.Professor
                                       join profMateria in _context.ProfessorMateria on prof.IdProfessor equals profMateria.IdProfessor
                                       join materia in _context.Materia on profMateria.IdMateria equals materia.IdMateria
                                       join materiaTurma in _context.MateriaTurma on materia.IdMateria equals materiaTurma.IdMateria
                                       join turma in _context.Turma on materiaTurma.IdTurma equals turma.IdTurma

                                       where prof.Cpf == login && prof.Senha == senha
                                       select new Professor
                                       {
                                           IdProfessor = prof.IdProfessor,
                                           Nome = prof.Nome,
                                           Cpf = prof.Cpf,
                                           Senha = prof.Senha,
                                           Materia = _context.Materia.Where(a =>a.IdMateria == materiaTurma.IdMateria).ToList(),
                                       Turma = turma
                                   }).Take(20).ToListAsync();
                

                return professor;
            }
            catch (Exception ex)
            {

            }
            return null;
        }


        // PUT: api/Professor/5
        [HttpPut]
        public async Task<IActionResult> PutProfessor( Professor professor)
        {
            

            _context.Entry(professor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProfessorExists(professor.IdProfessor))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProfessor", new { id = professor.IdProfessor }, professor);
        }

        // POST: api/Professor
        [HttpPost]
        public async Task<ActionResult<Professor>> PostProfessor(Professor professor)
        {
            try
            {
                _context.Professor.Add(professor);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProfessor", new { id = professor.IdProfessor }, professor);
            }
            catch (Exception ex) {
                return NoContent();
            }
        }

        // DELETE: api/Professor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Professor>> DeleteProfessor(int id)
        {
            var professor = await _context.Professor.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }

            _context.Professor.Remove(professor);
            await _context.SaveChangesAsync();

            return professor;
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professor.Any(e => e.IdProfessor == id);
        }
    }
}
