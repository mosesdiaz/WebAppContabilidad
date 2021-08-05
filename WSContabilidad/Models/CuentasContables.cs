using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSContabilidad.Models
{
    public class CuentasContables
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool PermiteMovimiento { get; set; }
        public int Tipo { get; set; }
        public int Balance { get; set; }
        public int? CtaMayor { get; set; }
        public CuentasContables CuentaContable { get; set; }
        public ICollection<CuentasContables> Cuentas { get; set; }
    }
}
