﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppContabilidad.Models
{
    public class TipoDeMoneda
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public double Tasa { get; set; }
        public bool Estado { get; set; }
    }
}
