using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppContabilidad.Data;
using WSContabilidad.Models;

namespace WebAppContabilidad.Controllers
{
    public class ReportesController : Controller
    {
        //private readonly TodoContext _context;
        private readonly WebAppContabilidadDbContext _context;
        public ReportesController(WebAppContabilidadDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PorCriterio()
        {
            return View(_context.Asientos.Include(a => a.CatalogoAuxiliar).Include(a => a.Monedas).ToList());
        }
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            /*var asiento = from a in _context.Asientos
                          where a.id == id
                          select new
                          {
                              a.CatalogoAuxiliar,
                              a.Descripcion,
                              a.Fecha,
                              a.Monedas,
                              a.TasaCambio,
                              a.Transacciones
                          };*/
            var asiento = await _context.Asientos
                .Include(a => a.CatalogoAuxiliar)
                .Include(a => a.Monedas)
                //.Include(a => a.Transacciones)
                .FirstOrDefaultAsync(m => m.id == id);
            if (asiento == null)
            {
                return NotFound();
            }

            return View(asiento);
        }

    }
}
