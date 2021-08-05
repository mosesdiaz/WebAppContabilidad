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
    public class AsientoesController : ControllerBase
    {
        private readonly TodoContext _context;

        public AsientoesController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Asientoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asiento>>> GetAsiento()
        {
            return await _context.Asiento.ToListAsync();
        }

        // GET: api/Asientoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asiento>> GetAsiento(int id)
        {
            var asiento = await _context.Asiento.FindAsync(id);

            if (asiento == null)
            {
                return NotFound();
            }

            return asiento;
        }

        // PUT: api/Asientoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsiento(int id, Asiento asiento)
        {
            if (id != asiento.id)
            {
                return BadRequest();
            }

            _context.Entry(asiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsientoExists(id))
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

        // POST: api/Asientoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asiento>> PostAsiento(Asiento asiento)
        {
            _context.Asiento.Add(asiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsiento", new { id = asiento.id }, asiento);
        }

        // DELETE: api/Asientoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsiento(int id)
        {
            var asiento = await _context.Asiento.FindAsync(id);
            if (asiento == null)
            {
                return NotFound();
            }

            _context.Asiento.Remove(asiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsientoExists(int id)
        {
            return _context.Asiento.Any(e => e.id == id);
        }
    }
}
