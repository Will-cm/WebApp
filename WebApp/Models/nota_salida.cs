using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class nota_salida
    {
        public int id { get; set; }
        public int nro_doc { get; set; }
        public DateOnly? fecha_emis { get; set; }
        public short? prioridad { get; set; }
        public short? programacion { get; set; }
        public string? cod_cliente { get; set; }
        public string? raz_social { get; set; }
        public string? cod_vendedor { get; set; }
        public short? estado { get; set; }
        public int? id_sucursal { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
