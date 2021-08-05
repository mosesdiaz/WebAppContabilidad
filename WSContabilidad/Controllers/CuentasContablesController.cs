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
    public class CuentasContablesController : ControllerBase
    {
        private readonly TodoContext _context;

        public CuentasContablesController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/CuentasContables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuentasContables>>> GetCuentasContables()
        {
            return await _context.CuentasContables.ToListAsync();
        }

        // GET: api/CuentasContables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CuentasContables>> GetCuentasContables(int id)
        {
            var cuentasContables = await _context.CuentasContables.FindAsync(id);

            if (cuentasContables == null)
            {
                return NotFound();
            }

            return cuentasContables;
        }

        // PUT: api/CuentasContables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuentasContables(int id, CuentasContables cuentasContables)
        {
            if (id != cuentasContables.Id)
            {
                return BadRequest();
            }

            _context.Entry(cuentasContables).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentasContablesExists(id))
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

        // POST: api/CuentasContables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CuentasContables>> PostCuentasContables(CuentasContables cuentasContables)
        {
            _context.CuentasContables.Add(cuentasContables);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuentasContables", new { id = cuentasContables.Id }, cuentasContables);
        }

        // DELETE: api/CuentasContables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuentasContables(int id)
        {
            var cuentasContables = await _context.CuentasContables.FindAsync(id);
            if (cuentasContables == null)
            {
                return NotFound();
            }

            _context.CuentasContables.Remove(cuentasContables);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuentasContablesExists(int id)
        {
            return _context.CuentasContables.Any(e => e.Id == id);
        }
    }
}
