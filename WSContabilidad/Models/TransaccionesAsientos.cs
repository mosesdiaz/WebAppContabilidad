using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSContabilidad.Models
{
    public class TransaccionesAsientos

    {
        [Key]
        public int Id_transaccion { get; set; }
        public int CuentasContablesId { get; set; }
        public int TipoMovimientoId { get; set; }
        public double Monto { get; set; }
        public int AsientoId { get; set; }

        /*public CuentasContables CuentasContables { get; set; }
        public TipoMovimiento TipoMovimientos { get; set; }*/
       // public Asiento Asiento { get; set; }

    }
}
