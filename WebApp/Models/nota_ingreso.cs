using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class nota_ingreso
    {
        public int id { get; set; }
        public int nro_doc { get; set; }
        public string? movimiento { get; set; }
        public string? usuario { get; set; }
        public short? estado { get; set; }
        public int? id_sucursal { get; set; }
        public int? id_suc_origen { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
