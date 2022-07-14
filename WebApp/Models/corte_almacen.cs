﻿using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class corte_almacen
    {
        public int id { get; set; }
        public int? id_corte { get; set; }
        public int? id_item { get; set; }
        public string? lote { get; set; }
        public string? serie { get; set; }
        public DateOnly? fecha_venc { get; set; }
        public string? ubicacion { get; set; }
        public decimal? cantidad { get; set; }
        public short? estado { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
