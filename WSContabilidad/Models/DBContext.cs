using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSContabilidad.Models;

namespace WSContabilidad.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<CuentasContables> CuentasContables { get; set; }
        public DbSet<CatalogoAuxiliar> CatalogoAuxiliares { get; set; }
        public DbSet<Monedas> Monedas { get; set; }
        public DbSet<TipoMovimiento> TipoMovimiento { get; set; }
        public DbSet<TipoCuenta> TiposCuenta { get; set; }
        public DbSet<Asiento> Asientos { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }
    }

}
