using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSContabilidad.Models
{
    public class Asiento
    {
        public int id { get; set; }
        public string Descripcion { get; set; }
        public int Auxiliar { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public int Moneda { get; set; }
        public double TasaCambio { get; set; }
        public int Cuenta { get; set; }
        public int TipoMovimiento { get; set; }
        public double Monto { get; set; }

        //public CatalogoAuxiliar CatalogoAuxiliar { get; set; }
        //public Monedas Moneda { get; set; }
        //public CuentasContables CuentasContables { get; set; }
        //public TipoMovimiento TipoMovimiento { get; set; }

    }
}
