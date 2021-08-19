using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppContabilidad.Models
{
    public class Asiento
    {
        [Key]
        public int id { get; set; }
        public string Descripcion { get; set; }
        public int CatalogoAuxiliarId { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public int MonedasId { get; set; }
        public double TasaCambio { get; set; }
        [Display(Name ="Sistema Auxiliar")]
        public SistemaAuxiliar CatalogoAuxiliar { get; set; }
        public TipoDeMoneda Monedas { get; set; }
        public List<TransaccionesAsientos> Transacciones { get; set; }

        /*public CuentaContable CuentaContable { get; set; }
        public TipoMovimiento TipoMovimiento { get; set; }*/
    }
}
