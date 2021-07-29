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
    public class TipoDeCuentasController : Controller
    {
        private readonly WebAppContabilidadDbContext _context;

        public TipoDeCuentasController(WebAppContabilidadDbContext context)
        {
            _context = context;
        }

        // GET: TipoDeCuentas
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoDeCuenta.ToListAsync());
        }

        // GET: TipoDeCuentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDeCuenta = await _context.TipoDeCuenta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoDeCuenta == null)
            {
                return NotFound();
            }

            return View(tipoDeCuenta);
        }

        // GET: TipoDeCuentas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoDeCuentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Origen,Estado")] TipoDeCuenta tipoDeCuenta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoDeCuenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDeCuenta);
        }

        // GET: TipoDeCuentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDeCuenta = await _context.TipoDeCuenta.FindAsync(id);
            if (tipoDeCuenta == null)
            {
                return NotFound();
            }
            return View(tipoDeCuenta);
        }

        // POST: TipoDeCuentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Origen,Estado")] TipoDeCuenta tipoDeCuenta)
        {
            if (id != tipoDeCuenta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoDeCuenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDeCuentaExists(tipoDeCuenta.Id))
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
            return View(tipoDeCuenta);
        }

        // GET: TipoDeCuentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDeCuenta = await _context.TipoDeCuenta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoDeCuenta == null)
            {
                return NotFound();
            }

            return View(tipoDeCuenta);
        }

        // POST: TipoDeCuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoDeCuenta = await _context.TipoDeCuenta.FindAsync(id);
            _context.TipoDeCuenta.Remove(tipoDeCuenta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoDeCuentaExists(int id)
        {
            return _context.TipoDeCuenta.Any(e => e.Id == id);
        }
    }
}
