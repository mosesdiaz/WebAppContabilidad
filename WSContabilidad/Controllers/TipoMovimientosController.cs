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
    public class TipoMovimientosController : ControllerBase
    {
        private readonly TodoContext _context;

        public TipoMovimientosController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TipoMovimientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoMovimiento>>> GetTipoMovimiento()
        {
            return await _context.TipoMovimiento.ToListAsync();
        }

        // GET: api/TipoMovimientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoMovimiento>> GetTipoMovimiento(int id)
        {
            var tipoMovimiento = await _context.TipoMovimiento.FindAsync(id);

            if (tipoMovimiento == null)
            {
                return NotFound();
            }

            return tipoMovimiento;
        }

        // PUT: api/TipoMovimientos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
/*        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoMovimiento(int id, TipoMovimiento tipoMovimiento)
        {
            if (id != tipoMovimiento.TipoMovimientoId)
            {
                return BadRequest();
            }

            _context.Entry(tipoMovimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoMovimientoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/TipoMovimientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
 /*       [HttpPost]
        public async Task<ActionResult<TipoMovimiento>> PostTipoMovimiento(TipoMovimiento tipoMovimiento)
        {
            _context.TipoMovimiento.Add(tipoMovimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoMovimiento", new { id = tipoMovimiento.TipoMovimientoId }, tipoMovimiento);
        }

        // DELETE: api/TipoMovimientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoMovimiento(int id)
        {
            var tipoMovimiento = await _context.TipoMovimiento.FindAsync(id);
            if (tipoMovimiento == null)
            {
                return NotFound();
            }

            _context.TipoMovimiento.Remove(tipoMovimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool TipoMovimientoExists(int id)
        {
            return _context.TipoMovimiento.Any(e => e.TipoMovimientoId == id);
        }
    }
}
