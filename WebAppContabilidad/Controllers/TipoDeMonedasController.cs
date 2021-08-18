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

        public async Task<IActionResult> UpdateCurrency()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://45.63.105.164:1337/exchange-rates/rates_by/DOP");
                //HTTP GET
                var responseTask = client.GetAsync("");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();

                    readTask.Wait();
                    var data = JsonConvert.DeserializeObject<dynamic>(readTask.Result);
                    Dictionary<string, double> dic = data.rates.ToObject<Dictionary<string, double>>();
                    foreach (KeyValuePair<string, double> currency in dic)
                    {
                        //currency is already added
                        if (_context.Monedas.Any(e => e.Codigo == currency.Key))
                        {
                            //var tipoDeMoneda = await _context.Monedas.Where(e => e.Codigo == currency.Key).ToListAsync();
                            TipoDeMoneda tipoDeMoneda = await _context.Monedas.FirstAsync(e => e.Codigo == currency.Key);
                            tipoDeMoneda.Tasa = currency.Value;
                            _context.Update(tipoDeMoneda);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            //last inserted id
                            //TipoDeMoneda ultimaMoneda = await _context.Monedas.LastAsync();
                            int ultimoId = _context.Monedas.Max(p => p.Id);

                            TipoDeMoneda nuevoTipoDeMoneda = new TipoDeMoneda()
                            {
                                Id = ultimoId + 1,
                                Descripcion = currency.Key,
                                Codigo = currency.Key,
                                Tasa = currency.Value,
                                Estado = true
                            };
                            _context.Add(nuevoTipoDeMoneda);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            return View("Index", await _context.Monedas.ToListAsync());
        }

        private bool TipoDeMonedaExists(int id)
        {
            return _context.Monedas.Any(e => e.Id == id);
        }
    }
}
