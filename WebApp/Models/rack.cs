using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class rack
    {
        public int id { get; set; }
        public string codigo { get; set; } = null!;
        public string descripcion { get; set; } = null!;
        public string? capacidad_max { get; set; }
        public string? cant_filas { get; set; }
        public string? cant_colum { get; set; }
        public short? estado { get; set; }
        public int? id_sucursal { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public virtual ICollection<almacen>? almacen { get; set; }  //add
    }
}
