using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSContabilidad.Models
{
    public class Monedas
    {
        public int id { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public double Tasa { get; set; }

        public List<Asiento> Asiento { get; set; }
    }
}
