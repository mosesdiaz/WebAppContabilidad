using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppContabilidad.Models
{
    public class CuentaContable
    {
        /*public CuentasContables()
        {
            InverseCuentaMayorNavigation = new HashSet<CuentasContables>();
        }*/
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Tipo { get; set; }
        public bool PermiteMovimiento { get; set; }
        public int? CuentaMayor { get; set; }
        public double Balance { get; set; }
        public bool Estado { get; set; }

        [ForeignKey("CuentaMayor")]
        public CuentaContable CuentaContables { get; set; }
        [ForeignKey("Tipo")]
        public TiposCuenta TiposCuenta { get; set; }
        public List<TransaccionesAsientos> Transacciones { get; set; }


        /*  [ForeignKey(nameof(CuentaMayor))]
          [InverseProperty(nameof(CuentasContables.InverseCuentaMayorNavigation))]
          public virtual CuentasContables CuentaMayorNavigation { get; set; }
          [ForeignKey(nameof(TipoDeCuenta))]
          [InverseProperty(nameof(WebAppContabilidad.Models.TipoDeCuenta.CuentasContables))]
          public virtual TipoDeCuenta TipoDeCuentaNavigation { get; set; }
          [InverseProperty(nameof(CuentasContables.CuentaMayorNavigation))]
          public virtual ICollection<CuentasContables> InverseCuentaMayorNavigation { get; set; }*/
    }
}
