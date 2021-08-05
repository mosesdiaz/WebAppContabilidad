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
    public class TipoCuentasController : ControllerBase
    {
        private readonly TodoContext _context;

        public TipoCuentasController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TipoCuentas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoCuenta>>> GetTiposCuenta()
        {
            return await _context.TiposCuenta.ToListAsync();
        }

        // GET: api/TipoCuentas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoCuenta>> GetTipoCuenta(int id)
        {
            var tipoCuenta = await _context.TiposCuenta.FindAsync(id);

            if (tipoCuenta == null)
            {
                return NotFound();
            }

            return tipoCuenta;
        }

        // PUT: api/TipoCuentas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoCuenta(int id, TipoCuenta tipoCuenta)
        {
            if (id != tipoCuenta.id)
            {
                return BadRequest();
            }

            _context.Entry(tipoCuenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoCuentaExists(id))
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

        // POST: api/TipoCuentas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoCuenta>> PostTipoCuenta(TipoCuenta tipoCuenta)
        {
            _context.TiposCuenta.Add(tipoCuenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoCuenta", new { id = tipoCuenta.id }, tipoCuenta);
        }

        // DELETE: api/TipoCuentas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoCuenta(int id)
        {
            var tipoCuenta = await _context.TiposCuenta.FindAsync(id);
            if (tipoCuenta == null)
            {
                return NotFound();
            }

            _context.TiposCuenta.Remove(tipoCuenta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoCuentaExists(int id)
        {
            return _context.TiposCuenta.Any(e => e.id == id);
        }
    }
}
