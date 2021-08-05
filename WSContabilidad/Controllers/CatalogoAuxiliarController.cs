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
    public class CatalogoAuxiliarController : ControllerBase
    {
        private readonly TodoContext _context;

        public CatalogoAuxiliarController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/CatalogoAuxiliar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogoAuxiliar>>> GetCatalogoAuxiliares()
        {
            return await _context.CatalogoAuxiliares.ToListAsync();
        }

        // GET: api/CatalogoAuxiliar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogoAuxiliar>> GetCatalogoAuxiliar(int id)
        {
            var catalogoAuxiliar = await _context.CatalogoAuxiliares.FindAsync(id);

            if (catalogoAuxiliar == null)
            {
                return NotFound();
            }

            return catalogoAuxiliar;
        }

        // PUT: api/CatalogoAuxiliar/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalogoAuxiliar(int id, CatalogoAuxiliar catalogoAuxiliar)
        {
            if (id != catalogoAuxiliar.id)
            {
                return BadRequest();
            }

            _context.Entry(catalogoAuxiliar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogoAuxiliarExists(id))
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

        // POST: api/CatalogoAuxiliar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CatalogoAuxiliar>> PostCatalogoAuxiliar(CatalogoAuxiliar catalogoAuxiliar)
        {
            _context.CatalogoAuxiliares.Add(catalogoAuxiliar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCatalogoAuxiliar", new { id = catalogoAuxiliar.id }, catalogoAuxiliar);
        }

        // DELETE: api/CatalogoAuxiliar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogoAuxiliar(int id)
        {
            var catalogoAuxiliar = await _context.CatalogoAuxiliares.FindAsync(id);
            if (catalogoAuxiliar == null)
            {
                return NotFound();
            }

            _context.CatalogoAuxiliares.Remove(catalogoAuxiliar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatalogoAuxiliarExists(int id)
        {
            return _context.CatalogoAuxiliares.Any(e => e.id == id);
        }
    }
}
