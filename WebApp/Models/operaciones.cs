using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class operaciones
    {
        public int id { get; set; }
        public int? id_rol { get; set; }
        public int? id_modulo { get; set; }
        public short? c { get; set; }
        public short? r { get; set; }
        public short? u { get; set; }
        public short? d { get; set; }
    }
}
