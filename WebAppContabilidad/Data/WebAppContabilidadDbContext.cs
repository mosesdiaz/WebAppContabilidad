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

        public DbSet<WebAppContabilidad.Models.TipoDeMoneda> TipoDeMoneda { get; set; }

        public DbSet<WebAppContabilidad.Models.TipoDeCuenta> TipoDeCuenta { get; set; }

        public DbSet<WebAppContabilidad.Models.SistemaAuxiliar> SistemaAuxiliar { get; set; }
        public DbSet<WebAppContabilidad.Models.CuentaContable> CuentaContable { get; set; }
    }
}
