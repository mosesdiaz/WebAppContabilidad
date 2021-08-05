using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSContabilidad.Models
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
        public CatalogoAuxiliar CatalogoAuxiliar { get; set; }
        public Monedas Monedas { get; set; }
        public List<TransaccionesAsientos> Transacciones { get; set; }

    }
}
