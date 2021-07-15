using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppContabilidad.Models
{
    public class CuentaContable
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public Origen Origen { get; set; }
    }
}
