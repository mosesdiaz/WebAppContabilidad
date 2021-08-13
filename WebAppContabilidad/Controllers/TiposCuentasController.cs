using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppContabilidad.Data;
using WebAppContabilidad.Models;

namespace WebAppContabilidad.Controllers
{
    public class TiposCuentasController : Controller
    {
        private readonly WebAppContabilidadDbContext _context;

        public TiposCuentasController(WebAppContabilidadDbContext context)
        {
            _context = context;
        }

        // GET: TiposCuentas
        public async Task<IActionResult> Index()
        {
            var webAppContabilidadDbContext = _context.TiposCuenta.Include(t => t.TipoMovimiento);
            return View(await webAppContabilidadDbContext.ToListAsync());
        }

        // GET: TiposCuentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposCuenta = await _context.TiposCuenta
                .Include(t => t.TipoMovimiento)
                .FirstOrDefaultAsync(m => m.id == id);
            if (tiposCuenta == null)
            {
                return NotFound();
            }

            return View(tiposCuenta);
        }

        // GET: TiposCuentas/Create
        public IActionResult Create()
        {
            ViewData["TipoMovimientoId"] = new SelectList(_context.Set<TipoMovimiento>(), "TipoMovimientoId", "Descripcion");
            return View();
        }

        // POST: TiposCuentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Descripcion,Estado,TipoMovimientoId")] TiposCuenta tiposCuenta)
        {
            if (ModelState.IsValid)
            {
                tiposCuenta.Estado = true;
                _context.Add(tiposCuenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoMovimientoId"] = new SelectList(_context.Set<TipoMovimiento>(), "TipoMovimientoId", "Descripcion", tiposCuenta.TipoMovimientoId);
            return View(tiposCuenta);
        }

        // GET: TiposCuentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposCuenta = await _context.TiposCuenta.FindAsync(id);
            if (tiposCuenta == null)
            {
                return NotFound();
            }
            ViewData["TipoMovimientoId"] = new SelectList(_context.Set<TipoMovimiento>(), "TipoMovimientoId", "Descripcion", tiposCuenta.TipoMovimientoId);
            return View(tiposCuenta);
        }

        // POST: TiposCuentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Descripcion,Estado,TipoMovimientoId")] TiposCuenta tiposCuenta)
        {
            if (id != tiposCuenta.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposCuenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposCuentaExists(tiposCuenta.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoMovimientoId"] = new SelectList(_context.Set<TipoMovimiento>(), "TipoMovimientoId", "TipoMovimientoId", tiposCuenta.TipoMovimientoId);
            return View(tiposCuenta);
        }

        // GET: TiposCuentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposCuenta = await _context.TiposCuenta
                .Include(t => t.TipoMovimiento)
                .FirstOrDefaultAsync(m => m.id == id);
            if (tiposCuenta == null)
            {
                return NotFound();
            }

            return View(tiposCuenta);
        }

        // POST: TiposCuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tiposCuenta = await _context.TiposCuenta.FindAsync(id);
            _context.TiposCuenta.Remove(tiposCuenta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiposCuentaExists(int id)
        {
            return _context.TiposCuenta.Any(e => e.id == id);
        }
    }
}
