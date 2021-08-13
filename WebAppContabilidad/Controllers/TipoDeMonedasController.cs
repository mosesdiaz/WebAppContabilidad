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
    public class TipoDeMonedasController : Controller
    {
        private readonly WebAppContabilidadDbContext _context;

        public TipoDeMonedasController(WebAppContabilidadDbContext context)
        {
            _context = context;
        }

        // GET: TipoDeMonedas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Monedas.ToListAsync());
        }

        // GET: TipoDeMonedas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDeMoneda = await _context.Monedas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoDeMoneda == null)
            {
                return NotFound();
            }

            return View(tipoDeMoneda);
        }

        // GET: TipoDeMonedas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoDeMonedas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Codigo,Tasa,Estado")] TipoDeMoneda tipoDeMoneda)
        {
            if (ModelState.IsValid)
            {
                tipoDeMoneda.Estado = true;
                _context.Add(tipoDeMoneda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDeMoneda);
        }

        // GET: TipoDeMonedas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDeMoneda = await _context.Monedas.FindAsync(id);
            if (tipoDeMoneda == null)
            {
                return NotFound();
            }
            return View(tipoDeMoneda);
        }

        // POST: TipoDeMonedas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Codigo,Tasa,Estado")] TipoDeMoneda tipoDeMoneda)
        {
            if (id != tipoDeMoneda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoDeMoneda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDeMonedaExists(tipoDeMoneda.Id))
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
            return View(tipoDeMoneda);
        }

        // GET: TipoDeMonedas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDeMoneda = await _context.Monedas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoDeMoneda == null)
            {
                return NotFound();
            }

            return View(tipoDeMoneda);
        }

        // POST: TipoDeMonedas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoDeMoneda = await _context.Monedas.FindAsync(id);
            _context.Monedas.Remove(tipoDeMoneda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoDeMonedaExists(int id)
        {
            return _context.Monedas.Any(e => e.Id == id);
        }
    }
}
