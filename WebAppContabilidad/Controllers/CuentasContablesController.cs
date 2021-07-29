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
    public class CuentasContablesController : Controller
    {
        private readonly WebAppContabilidadDbContext _context;

        public CuentasContablesController(WebAppContabilidadDbContext context)
        {
            _context = context;
        }

        // GET: CuentasContables
        public async Task<IActionResult> Index()
        {
            var webAppContabilidadDbContext = _context.CuentaContable.Include(c => c.CuentaMayorNavigation).Include(c => c.TipoDeCuentaNavigation);
            return View(await webAppContabilidadDbContext.ToListAsync());
        }

        // GET: CuentasContables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuentaContable = await _context.CuentaContable
                .Include(c => c.CuentaMayorNavigation)
                .Include(c => c.TipoDeCuentaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuentaContable == null)
            {
                return NotFound();
            }

            return View(cuentaContable);
        }

        // GET: CuentasContables/Create
        public IActionResult Create()
        {
            ViewData["CuentaMayor"] = new SelectList(_context.CuentaContable, "Id", "Descripcion");
            ViewData["TipoDeCuenta"] = new SelectList(_context.TipoDeCuenta, "Id", "Descripcion");
            return View();
        }

        // POST: CuentasContables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,TipoDeCuenta,PermiteTransacciones,CuentaMayor,Balance,Estado")] CuentaContable cuentaContable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cuentaContable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CuentaMayor"] = new SelectList(_context.CuentaContable, "Id", "Id", cuentaContable.CuentaMayor);
            ViewData["TipoDeCuenta"] = new SelectList(_context.TipoDeCuenta, "Id", "Id", cuentaContable.TipoDeCuenta);
            return View(cuentaContable);
        }

        // GET: CuentasContables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuentaContable = await _context.CuentaContable.FindAsync(id);
            if (cuentaContable == null)
            {
                return NotFound();
            }
            ViewData["CuentaMayor"] = new SelectList(_context.CuentaContable, "Id", "Id", cuentaContable.CuentaMayor);
            ViewData["TipoDeCuenta"] = new SelectList(_context.TipoDeCuenta, "Id", "Id", cuentaContable.TipoDeCuenta);
            return View(cuentaContable);
        }

        // POST: CuentasContables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,TipoDeCuenta,PermiteTransacciones,CuentaMayor,Balance,Estado")] CuentaContable cuentaContable)
        {
            if (id != cuentaContable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuentaContable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuentaContableExists(cuentaContable.Id))
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
            ViewData["CuentaMayor"] = new SelectList(_context.CuentaContable, "Id", "Id", cuentaContable.CuentaMayor);
            ViewData["TipoDeCuenta"] = new SelectList(_context.TipoDeCuenta, "Id", "Id", cuentaContable.TipoDeCuenta);
            return View(cuentaContable);
        }

        // GET: CuentasContables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuentaContable = await _context.CuentaContable
                .Include(c => c.CuentaMayorNavigation)
                .Include(c => c.TipoDeCuentaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuentaContable == null)
            {
                return NotFound();
            }

            return View(cuentaContable);
        }

        // POST: CuentasContables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuentaContable = await _context.CuentaContable.FindAsync(id);
            _context.CuentaContable.Remove(cuentaContable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuentaContableExists(int id)
        {
            return _context.CuentaContable.Any(e => e.Id == id);
        }
    }
}
