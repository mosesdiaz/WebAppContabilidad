using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppContabilidad.Models;

namespace WebAppContabilidad.Data
{
    public class WebAppContabilidadDbContext : DbContext
    {
        public WebAppContabilidadDbContext (DbContextOptions<WebAppContabilidadDbContext> options)
            : base(options)
        {
        }

        public DbSet<TipoDeMoneda> Monedas { get; set; }
        public DbSet<SistemaAuxiliar> CatalogoAuxiliares { get; set; }
        public DbSet<CuentaContable> CuentasContables { get; set; }
        public DbSet<TiposCuenta> TiposCuenta { get; set; }
        public DbSet<TipoMovimiento> TipoMovimiento { get; set; }
        public DbSet<Asiento> Asientos { get; set; }
        public DbSet<TransaccionesAsientos> TransaccionesAsientos { get; set; }
    }
}
