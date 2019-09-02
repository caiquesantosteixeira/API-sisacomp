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
    public class AdministradorController : ControllerBase
    {
        private readonly DBContext _context;

        public AdministradorController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Administrador
        [HttpGet]
        [Route("Login")]
        public async Task<ActionResult<IEnumerable<Administrador>>> GetAdministrador(string login,string senha)
        {
            var admins = await (from admin in _context.Administrador
                                     where admin.Cpf == login && admin.Senha == senha
                                     select new Administrador
                                     {
                                         IdAdministrador = admin.IdAdministrador,
                                         Nome = admin.Nome,
                                         Cpf = admin.Cpf,
                                         Senha = admin.Senha
                                     }).Take(20).ToListAsync();
            return admins;
        }

        // GET: api/Administrador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Administrador>> GetAdministrador(int id)
        {
            var administrador = await _context.Administrador.FindAsync(id);

            if (administrador == null)
            {
                return NotFound();
            }

            return administrador;
        }

        // PUT: api/Administrador/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrador(int id, Administrador administrador)
        {
            if (id != administrador.IdAdministrador)
            {
                return BadRequest();
            }

            _context.Entry(administrador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministradorExists(id))
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

        // POST: api/Administrador
        [HttpPost]
        public async Task<ActionResult<Administrador>> PostAdministrador(Administrador administrador)
        {
            _context.Administrador.Add(administrador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdministrador", new { id = administrador.IdAdministrador }, administrador);
        }

        // DELETE: api/Administrador/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Administrador>> DeleteAdministrador(int id)
        {
            var administrador = await _context.Administrador.FindAsync(id);
            if (administrador == null)
            {
                return NotFound();
            }

            _context.Administrador.Remove(administrador);
            await _context.SaveChangesAsync();

            return administrador;
        }

        private bool AdministradorExists(int id)
        {
            return _context.Administrador.Any(e => e.IdAdministrador == id);
        }
    }
}
