using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class corte
    {
        public int id { get; set; }
        public int nro { get; set; }
        public DateOnly? fec_inicio { get; set; }
        public DateOnly? fec_final { get; set; }
        public short? tip_inventario { get; set; }
        public short? estado { get; set; }
        public int? id_sucursal { get; set; }
    }
}
