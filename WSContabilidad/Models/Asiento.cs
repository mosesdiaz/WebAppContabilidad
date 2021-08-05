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
        public int AuxiliarId { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public int MonedaId { get; set; }
        public float TasaCambio { get; set; }
        public int CuentaId { get; set; }
        public int TipoMovimientoID { get; set; }
        public float Monto { get; set; }

        public CatalogoAuxiliar CatalogoAuxiliar { get; set; }
        public Monedas Moneda { get; set; }
        public CuentasContables CuentasContables { get; set; }
        public TipoMovimiento TipoMovimiento { get; set; }

    }
}
