using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebAppContabilidad.Models;

namespace WebAppContabilidad.Models
{
    public class TiposCuenta
    {
        [Key]
        public int id { get; set; }
        public string Descripcion { get; set; }

        public bool Estado { get; set; }
        public int TipoMovimientoId { get; set; }


        [ForeignKey("TipoMovimientoId")]
        public TipoMovimiento TipoMovimiento { get; set; }

    }
}
