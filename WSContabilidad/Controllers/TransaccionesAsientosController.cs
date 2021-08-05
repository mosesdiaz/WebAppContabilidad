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
    public class TransaccionesAsientosController : ControllerBase
    {
        private readonly TodoContext _context;

        public TransaccionesAsientosController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TransaccionesAsientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransaccionesAsientos>>> GetTransaccionesAsientos()
        {
            return await _context.TransaccionesAsientos.ToListAsync();
        }

        // GET: api/TransaccionesAsientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransaccionesAsientos>> GetTransaccionesAsientos(int id)
        {
            var transaccionesAsientos = await _context.TransaccionesAsientos.FindAsync(id);

            if (transaccionesAsientos == null)
            {
                return NotFound();
            }

            return transaccionesAsientos;
        }

        // PUT: api/TransaccionesAsientos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaccionesAsientos(int id, TransaccionesAsientos transaccionesAsientos)
        {
            if (id != transaccionesAsientos.Id_transaccion)
            {
                return BadRequest();
            }

            _context.Entry(transaccionesAsientos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransaccionesAsientosExists(id))
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

        // POST: api/TransaccionesAsientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TransaccionesAsientos>> PostTransaccionesAsientos(TransaccionesAsientos transaccionesAsientos)
        {
            _context.TransaccionesAsientos.Add(transaccionesAsientos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaccionesAsientos", new { id = transaccionesAsientos.Id_transaccion }, transaccionesAsientos);
        }

        // DELETE: api/TransaccionesAsientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaccionesAsientos(int id)
        {
            var transaccionesAsientos = await _context.TransaccionesAsientos.FindAsync(id);
            if (transaccionesAsientos == null)
            {
                return NotFound();
            }

            _context.TransaccionesAsientos.Remove(transaccionesAsientos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransaccionesAsientosExists(int id)
        {
            return _context.TransaccionesAsientos.Any(e => e.Id_transaccion == id);
        }
    }
}
