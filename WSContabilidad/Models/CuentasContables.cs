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
        public int? CuentaMayor { get; set; }
        //public CuentasContables CuentaContable { get; set; }
        //public List<CuentasContables> CuentasContable { get; set; }
        //public List<TransaccionesAsientos> Transacciones { get; set; }
    }
}
