using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppContabilidad.Models
{
    public class Moneda
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string TasaDeCambio { get; set; }
    }
}
