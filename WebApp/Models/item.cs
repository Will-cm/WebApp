using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class item
    {
        public int id { get; set; }
        public string codigo { get; set; } = null!;
        public string descripcion { get; set; } = null!;
        public string? unidad_med { get; set; }
        public decimal? precio { get; set; }
        public int? maximo { get; set; }
        public int? minimo { get; set; }
        public int? vida_util { get; set; }
        public short? estado { get; set; }
        public string? cod_sbcategoria { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
