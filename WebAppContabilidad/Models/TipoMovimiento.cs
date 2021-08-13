using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppContabilidad.Models
{
    public class TipoMovimiento
    {
        [Key]
        public int  TipoMovimientoId { get; set; }
        public string  Descripcion { get; set; }

    }
}
