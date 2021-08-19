using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSContabilidad.Models;

namespace WSContabilidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedasController : ControllerBase
    {
        private readonly TodoContext _context;

        public MonedasController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Monedas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Monedas>>> GetMonedas()
        {
            return await _context.Monedas.ToListAsync();
        }

        // GET: api/Monedas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Monedas>> GetMonedas(int id)
        {
            var monedas = await _context.Monedas.FindAsync(id);

            if (monedas == null)
            {
                return NotFound();
            }

            return monedas;
        }

        // PUT: api/Monedas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
 /*       [HttpPut("{id}")]
        public async Task<IActionResult> PutMonedas(int id, Monedas monedas)
        {
            if (id != monedas.id)
            {
                return BadRequest();
            }

            _context.Entry(monedas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonedasExists(id))
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

        // POST: api/Monedas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Monedas>> PostMonedas(Monedas monedas)
        {
            _context.Monedas.Add(monedas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMonedas", new { id = monedas.id }, monedas);
        }

        // DELETE: api/Monedas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonedas(int id)
        {
            var monedas = await _context.Monedas.FindAsync(id);
            if (monedas == null)
            {
                return NotFound();
            }

            _context.Monedas.Remove(monedas);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool MonedasExists(int id)
        {
            return _context.Monedas.Any(e => e.id == id);
        }
    }
}
