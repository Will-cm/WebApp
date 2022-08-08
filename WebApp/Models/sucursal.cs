using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class sucursal
    {
        public int id { get; set; }
        public string codigo { get; set; } = null!;
        public string descripcion { get; set; } = null!;
        public string? ubicacion { get; set; }
        public string? direccion { get; set; }
        public short? estado { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public virtual ICollection<nota_ingreso>? nota_ingreso { get; set; }  //add
        public virtual ICollection<nota_salida>? nota_salida { get; set; }  //add
    }
}
