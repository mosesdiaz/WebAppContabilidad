using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WSContabilidad.Models
{
    public class TipoCuenta
    {
        [Key]
        public int id { get; set; }
        public string Descripcion { get; set; }
        public int TipoMovimientoId { get; set; }
        //public TipoMovimiento TipoMovimiento { get; set; }

    }
}
