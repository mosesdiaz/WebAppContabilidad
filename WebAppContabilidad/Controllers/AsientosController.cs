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
                           }).AsEnumerable().FirstOrDefault();*/
            var asiento = await _context.Asientos
                 .Include(a => a.CatalogoAuxiliar)
                 .Include(a => a.Monedas)
                 .Include(a => a.Transacciones)
                 .FirstOrDefaultAsync(m => m.id == id);
            if (asiento == null)
            {
                return NotFound();
            }

            ViewData["Transacciones"] = _context.TransaccionesAsientos
                .Include(x=>x.CuentaContable)
                .Include(x=>x.TipoMovimiento)
                .Where(x => x.AsientoId == id);

            return View(asiento);
        }

        // GET: Asientos/Create
        public IActionResult Create()
        {
            //asientos
            ViewData["CatalogoAuxiliarId"] = new SelectList(_context.CatalogoAuxiliares, "Id", "Descripcion");
            ViewData["MonedasId"] = new SelectList(_context.Monedas, "Id", "Descripcion");

            //transacciones
            var cdm = _context.CuentasContables.Where(x => x.PermiteMovimiento == true).Where(x => x.TiposCuenta.TipoMovimientoId == 1);
            var ccm = _context.CuentasContables.Where(x => x.PermiteMovimiento == true).Where(x => x.TiposCuenta.TipoMovimientoId == 2);            
            ViewData["CuentaCredito"] = ccm.AsEnumerable();
            ViewData["CuentaDebito"] =cdm.AsEnumerable();
            return View();
        }

        // POST: Asientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Descripcion,CatalogoAuxiliarId,Fecha,Estado,MonedasId,TasaCambio")] Asiento asiento,
            TransaccionesAsientos transaccionesAsientos1, TransaccionesAsientos transaccionesAsientos2,
            double monto1, double monto2, int CuentaCredito, int CuentaDebito)
        {
            if (ModelState.IsValid)
            {
                #region guardar asiento y transacciones
                //crear asiento
                asiento.Estado = "R";
                asiento.TasaCambio = _context.Monedas.Where(x => x.Id == asiento.MonedasId)
                                .Select(x => x.Tasa).FirstOrDefault();
                _context.Add(asiento);
                await _context.SaveChangesAsync();

                //crear transaccion 1
                transaccionesAsientos1.Monto = monto1;
                transaccionesAsientos1.CuentasContablesId = CuentaCredito;
                transaccionesAsientos1.AsientoId = asiento.id;
                
                var cc = _context.CuentasContables
                    .Where(x=>x.Id==CuentaCredito)
                    .Select(x => x.TiposCuenta.TipoMovimientoId).FirstOrDefault();//tipo de movimiento de la cuenta contable
                transaccionesAsientos1.TipoMovimientoId = _context.TipoMovimiento
                    .Where(x => x.TipoMovimientoId == cc)
                    .Select(x => x.TipoMovimientoId).FirstOrDefault();

                _context.Add(transaccionesAsientos1);
                
                //crear transaccion 2
                transaccionesAsientos2.Monto = monto2;
                transaccionesAsientos2.CuentasContablesId = CuentaDebito;
                transaccionesAsientos2.AsientoId = asiento.id;

                cc = _context.CuentasContables
                    .Where(x => x.Id == CuentaDebito)
                    .Select(x => x.TiposCuenta.TipoMovimientoId).FirstOrDefault();//tipo de movimiento de la cuenta contable

                transaccionesAsientos2.TipoMovimientoId = _context.TipoMovimiento
                    .Where(x => x.TipoMovimientoId == cc)
                    .Select(x => x.TipoMovimientoId).FirstOrDefault();
                _context.Add(transaccionesAsientos2);
                await _context.SaveChangesAsync();
                #endregion

                return RedirectToAction(nameof(Index));
            }
            #region viewdata
            ViewData["CatalogoAuxiliarId"] = new SelectList(_context.CatalogoAuxiliares, "Id", "Descripcion", asiento.CatalogoAuxiliarId);
            ViewData["MonedasId"] = new SelectList(_context.Monedas, "Id", "Descripcion", asiento.MonedasId);

            var cdm = _context.CuentasContables.Where(x => x.PermiteMovimiento == true).Where(x => x.TiposCuenta.TipoMovimientoId == 1);
            var ccm = _context.CuentasContables.Where(x => x.PermiteMovimiento == true).Where(x => x.TiposCuenta.TipoMovimientoId == 2);
            ViewData["CuentaCredito"] = new SelectList(ccm, "Id", "Descripcion", transaccionesAsientos1.CuentasContablesId);
            ViewData["CuentaDebito"] = new SelectList(cdm, "Id", "Descripcion", transaccionesAsientos2.CuentasContablesId);
            #endregion

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

        /*public ActionResult ViewTransacciones(int asiento)
        {
            var transacciones = _context.TransaccionesAsientos;
                //.Where(x => x.AsientoId == asiento);
            return PartialView(transacciones);
        }*/
    }
}
