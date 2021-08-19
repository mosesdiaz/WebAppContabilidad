using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAppContabilidad.Data;
using WebAppContabilidad.Models;

namespace WebAppContabilidad.Controllers
{
    public class AsientosController : Controller
    {
        private readonly WebAppContabilidadDbContext _context;

        public AsientosController(WebAppContabilidadDbContext context)
        {
            _context = context;
        }

        // GET: Asientos
        public async Task<IActionResult> Index()
        {
            var webAppContabilidadDbContext = _context.Asientos.Include(a => a.CatalogoAuxiliar).Include(a => a.Monedas);
            return View(await webAppContabilidadDbContext.ToListAsync());
        }

        // GET: Asientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            #region asiento
            /*var asiento = (from a in _context.Asientos
                           where a.id == id
                           select new
                           {
                               a.id,
                               a.CatalogoAuxiliar,
                               a.Descripcion,
                               a.Fecha,
                               a.Monedas,
                               a.TasaCambio,
                               Transacciones = from t in _context.TransaccionesAsientos
                                               where a.id == t.AsientoId
                                               select new
                                               {
                                                   t.CuentaContable,
                                                   t.Monto,
                                                   t.TipoMovimiento
                                               }
                           }).AsEnumerable().ToList().FirstOrDefault();*/
            var asiento = await _context.Asientos
                 .Include(a => a.CatalogoAuxiliar)
                 .Include(a => a.Monedas)
                 .Include(a => a.Transacciones)
                 .FirstOrDefaultAsync(m => m.id == id);
            #endregion
            if (asiento == null)
            {
                return NotFound();
            }

            return View(asiento);
        }

        // GET: Asientos/Create
        public IActionResult Create()
        {
            var cdm = _context.CuentasContables.Where(x => x.PermiteMovimiento == true).Where(x => x.TiposCuenta.TipoMovimientoId == 1);
            var ccm = _context.CuentasContables.Where(x => x.PermiteMovimiento == true).Where(x => x.TiposCuenta.TipoMovimientoId == 2);
            ViewData["CatalogoAuxiliarId"] = new SelectList(_context.CatalogoAuxiliares, "Id", "Id");
            ViewData["MonedasId"] = new SelectList(_context.Monedas, "Id", "Id");
            ViewData["CuentaCredito"] = new SelectList(ccm, "Id", "Descripcion");
            ViewData["CuentaDebito"] = new SelectList(cdm, "Id", "Descripcion");
            return View();
        }

        // POST: Asientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Descripcion,CatalogoAuxiliarId,Fecha,Estado,MonedasId,TasaCambio")] Asiento asiento,
            TransaccionesAsientos transaccionesAsientos, double monto1, double monto2, int CuentaCredito, int CuentaDebito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asiento);
                await _context.SaveChangesAsync();

                transaccionesAsientos.Monto = monto1;
                transaccionesAsientos.CuentasContablesId = CuentaCredito;
                transaccionesAsientos.AsientoId = asiento.id;
                transaccionesAsientos.TipoMovimientoId = (int) _context.TipoMovimiento.Where(x => x.TipoMovimientoId == CuentaCredito)
                    .Select(x => x.TipoMovimientoId).FirstOrDefault();
                await _context.SaveChangesAsync();

                transaccionesAsientos.Monto = monto2;
                transaccionesAsientos.CuentasContablesId = CuentaDebito;
                transaccionesAsientos.AsientoId = asiento.id;
                transaccionesAsientos.TipoMovimientoId = (int)_context.TipoMovimiento.Where(x => x.TipoMovimientoId == CuentaDebito)
                    .Select(x => x.TipoMovimientoId).FirstOrDefault();
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["CatalogoAuxiliarId"] = new SelectList(_context.CatalogoAuxiliares, "Id", "Id", asiento.CatalogoAuxiliarId);
            ViewData["MonedasId"] = new SelectList(_context.Monedas, "Id", "Id", asiento.MonedasId);

            var cdm = _context.CuentasContables.Where(x => x.PermiteMovimiento == true).Where(x => x.TiposCuenta.TipoMovimientoId == 1);
            var ccm = _context.CuentasContables.Where(x => x.PermiteMovimiento == true).Where(x => x.TiposCuenta.TipoMovimientoId == 2);
            ViewData["CuentaCredito"] = new SelectList(ccm, "Id", "Descripcion");
            ViewData["CuentaDebito"] = new SelectList(cdm, "Id", "Descripcion");
            return View(asiento);
        }

        // GET: Asientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asiento = await _context.Asientos.FindAsync(id);
            if (asiento == null)
            {
                return NotFound();
            }
            ViewData["CatalogoAuxiliarId"] = new SelectList(_context.CatalogoAuxiliares, "Id", "Id", asiento.CatalogoAuxiliarId);
            ViewData["MonedasId"] = new SelectList(_context.Monedas, "Id", "Id", asiento.MonedasId);
            return View(asiento);
        }

        // POST: Asientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Descripcion,CatalogoAuxiliarId,Fecha,Estado,MonedasId,TasaCambio")] Asiento asiento)
        {
            if (id != asiento.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsientoExists(asiento.id))
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
            ViewData["CatalogoAuxiliarId"] = new SelectList(_context.CatalogoAuxiliares, "Id", "Id", asiento.CatalogoAuxiliarId);
            ViewData["MonedasId"] = new SelectList(_context.Monedas, "Id", "Id", asiento.MonedasId);
            return View(asiento);
        }

        // GET: Asientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asiento = await _context.Asientos
                .Include(a => a.CatalogoAuxiliar)
                .Include(a => a.Monedas)
                .FirstOrDefaultAsync(m => m.id == id);
            if (asiento == null)
            {
                return NotFound();
            }

            return View(asiento);
        }

        // POST: Asientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asiento = await _context.Asientos.FindAsync(id);
            _context.Asientos.Remove(asiento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsientoExists(int id)
        {
            return _context.Asientos.Any(e => e.id == id);
        }
    }
}
