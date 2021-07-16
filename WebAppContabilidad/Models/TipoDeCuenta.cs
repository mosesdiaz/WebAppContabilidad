using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppContabilidad.Models
{
    public enum Origen
    {
        DB, CR
    }
    public class TipoDeCuenta
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public Origen Origen { get; set; }
        public bool Estado { get; set; }


        public ICollection<CuentaContable> CuentaContable { get; set; }
    }
}
