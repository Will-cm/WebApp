using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class picking_nsalida
    {
        public int id { get; set; }
        public int? id_nsalida { get; set; }
        public int? id_item { get; set; }
        public string? lote { get; set; }
        public DateOnly? fecha_venc { get; set; }
        public string? serie { get; set; }
        public decimal cantidad { get; set; }
        public decimal? cant_picking { get; set; }
        public string? usuario { get; set; }
        public short? estado { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
