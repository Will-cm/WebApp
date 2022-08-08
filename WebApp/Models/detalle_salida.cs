using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class detalle_salida
    {
        public int id { get; set; }
        public int? id_nsalida { get; set; }
        public int? id_item { get; set; }
        public string? lote { get; set; }
        public DateOnly? fecha_venc { get; set; }
        public string? serie { get; set; }
        public decimal cantidad { get; set; }
        public short? estado { get; set; }
        public virtual item? item { get; set; } //add
        public virtual nota_salida? nota_salida { get; set; } //add
    }
}
