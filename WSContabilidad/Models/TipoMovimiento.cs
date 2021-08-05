using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSContabilidad.Models
{
    public class TipoMovimiento
    {
        public TipoMovimiento()
        {
            TipoCuentas = new HashSet<TipoCuenta>();
        }
        [Key]
        public int TipoMovimientoId { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TipoCuenta> TipoCuentas { get; set; }
        public ICollection<Asiento> Asientos { get; set; }
    }
}
