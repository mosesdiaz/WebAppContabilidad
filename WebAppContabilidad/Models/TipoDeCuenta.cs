using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppContabilidad.Models
{
    public enum Origen
    {
        DB, CR
    }
    public class TipoDeCuenta
    {
        public TipoDeCuenta()
        {
            CuentasContables = new HashSet<CuentaContable>();
        }
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public Origen Origen { get; set; }
        public bool Estado { get; set; }


        [InverseProperty(nameof(CuentaContable.TipoDeCuentaNavigation))]
        public virtual ICollection<CuentaContable> CuentasContables { get; set; }
    }
}
