using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppContabilidad.Models
{
    public class CuentaContable
    {
        public CuentaContable()
        {
            InverseCuentaMayorNavigation = new HashSet<CuentaContable>();
        }
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public int? TipoDeCuenta { get; set; }


        public bool PermiteTransacciones { get; set; }

        public int? CuentaMayor { get; set; }

        [Column(TypeName = "decimal(11, 2)")]
        public decimal Balance { get; set; }
        public bool Estado { get; set; }


        [ForeignKey(nameof(CuentaMayor))]
        [InverseProperty(nameof(CuentaContable.InverseCuentaMayorNavigation))]
        public virtual CuentaContable CuentaMayorNavigation { get; set; }
        [ForeignKey(nameof(TipoDeCuenta))]
        [InverseProperty(nameof(WebAppContabilidad.Models.TipoDeCuenta.CuentasContables))]
        public virtual TipoDeCuenta TipoDeCuentaNavigation { get; set; }
        [InverseProperty(nameof(CuentaContable.CuentaMayorNavigation))]
        public virtual ICollection<CuentaContable> InverseCuentaMayorNavigation { get; set; }
    }
}
