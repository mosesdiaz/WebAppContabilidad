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
    public class TipoMovimientoesController : ControllerBase
    {
        private readonly TodoContext _context;

        public TipoMovimientoesController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TipoMovimientoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoMovimiento>>> GetTipoMovimiento()
        {
            return await _context.TipoMovimiento.ToListAsync();
        }

        // GET: api/TipoMovimientoes/5
        [HttpGet("{TipoMovimientoId}")]
        public async Task<ActionResult<TipoMovimiento>> GetTipoMovimiento(int TipoMovimientoId)
        {
            var tipoMovimiento = await _context.TipoMovimiento.FindAsync(TipoMovimientoId);

            if (tipoMovimiento == null)
            {
                return NotFound();
            }

            return tipoMovimiento;
        }

        // PUT: api/TipoMovimientoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkTipoMovimientoId=2123754
        [HttpPut("{TipoMovimientoId}")]
        public async Task<IActionResult> PutTipoMovimiento(int TipoMovimientoId, TipoMovimiento tipoMovimiento)
        {
            if (TipoMovimientoId != tipoMovimiento.TipoMovimientoId)
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
                if (!TipoMovimientoExists(TipoMovimientoId))
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

        // POST: api/TipoMovimientoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkTipoMovimientoId=2123754
        [HttpPost]
        public async Task<ActionResult<TipoMovimiento>> PostTipoMovimiento(TipoMovimiento tipoMovimiento)
        {
            _context.TipoMovimiento.Add(tipoMovimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoMovimiento", new { TipoMovimientoId = tipoMovimiento.TipoMovimientoId }, tipoMovimiento);
        }

        // DELETE: api/TipoMovimientoes/5
        [HttpDelete("{TipoMovimientoId}")]
        public async Task<IActionResult> DeleteTipoMovimiento(int TipoMovimientoId)
        {
            var tipoMovimiento = await _context.TipoMovimiento.FindAsync(TipoMovimientoId);
            if (tipoMovimiento == null)
            {
                return NotFound();
            }

            _context.TipoMovimiento.Remove(tipoMovimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoMovimientoExists(int TipoMovimientoId)
        {
            return _context.TipoMovimiento.Any(e => e.TipoMovimientoId == TipoMovimientoId);
        }
    }
}
