using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppContabilidad.Models
{
    public class TransaccionesAsientos
    {
        [Key]
        public int Id_transaccion { get; set; }
        public int CuentasContablesId { get; set; }
        public int TipoMovimientoId { get; set; }
        public double Monto { get; set; }
        public int AsientoId { get; set; }

        //[ForeignKey("CuentasContablesId")]
        public virtual CuentaContable CuentaContable{ get; set; }
        //[ForeignKey("TipoMovimientoId")]
        public virtual TipoMovimiento TipoMovimiento { get; set; }
    }
}
