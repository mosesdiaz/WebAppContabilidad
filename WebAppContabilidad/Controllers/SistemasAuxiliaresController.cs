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
    public class SistemasAuxiliaresController : Controller
    {
        private readonly WebAppContabilidadDbContext _context;

        public SistemasAuxiliaresController(WebAppContabilidadDbContext context)
        {
            _context = context;
        }

        // GET: SistemasAuxiliares
        public async Task<IActionResult> Index()
        {
            return View(await _context.CatalogoAuxiliares.ToListAsync());
        }

        // GET: SistemasAuxiliares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sistemaAuxiliar = await _context.CatalogoAuxiliares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sistemaAuxiliar == null)
            {
                return NotFound();
            }

            return View(sistemaAuxiliar);
        }

        // GET: SistemasAuxiliares/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SistemasAuxiliares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Estado")] SistemaAuxiliar sistemaAuxiliar)
        {
            if (ModelState.IsValid)
            {
                sistemaAuxiliar.Estado = true;
                _context.Add(sistemaAuxiliar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sistemaAuxiliar);
        }

        // GET: SistemasAuxiliares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sistemaAuxiliar = await _context.CatalogoAuxiliares.FindAsync(id);
            if (sistemaAuxiliar == null)
            {
                return NotFound();
            }
            return View(sistemaAuxiliar);
        }

        // POST: SistemasAuxiliares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Estado")] SistemaAuxiliar sistemaAuxiliar)
        {
            if (id != sistemaAuxiliar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sistemaAuxiliar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SistemaAuxiliarExists(sistemaAuxiliar.Id))
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
            return View(sistemaAuxiliar);
        }

        // GET: SistemasAuxiliares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sistemaAuxiliar = await _context.CatalogoAuxiliares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sistemaAuxiliar == null)
            {
                return NotFound();
            }

            return View(sistemaAuxiliar);
        }

        // POST: SistemasAuxiliares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sistemaAuxiliar = await _context.CatalogoAuxiliares.FindAsync(id);
            _context.CatalogoAuxiliares.Remove(sistemaAuxiliar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SistemaAuxiliarExists(int id)
        {
            return _context.CatalogoAuxiliares.Any(e => e.Id == id);
        }
    }
}
