using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppContabilidad.Models
{
    public class CuentaContable
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        //Foreign Key Tipo de cuenta
        [ForeignKey("TipoDeCuenta")]
        public int TipoDeCuentaId { get; set; }
        public TipoDeCuenta TipoDeCuenta { get; set; }


        public bool PermiteTransacciones { get; set; }

        //Foreign Key Cuenta Mayor
        [ForeignKey("CuentaMayor")]
        public int CuentaMayorId { get; set; }
        public CuentaContable CuentaMayor { get; set; }


        public decimal Balance { get; set; }
        public bool Estado { get; set; }

        public ICollection<CuentaContable> CuentasContables { get; set; }
    }
}
