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
        public int? cod_sbcategoria { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }

        public virtual subcategoria? subcategoria { get; set; } //add

        public virtual ICollection<corte_almacen>? corte_almacen { get; set; }  //add
        public virtual ICollection<inventario>? inventarios { get; set; }  //add
        public virtual ICollection<almacen>? almacen { get; set; }  //add
        public virtual ICollection<detalle_ingreso>? detalle_ingreso { get; set; }  //add
        public virtual ICollection<detalle_salida>? detalle_salida { get; set; }  //add
        public virtual ICollection<picking_nsalida>? picking_nsalida { get; set; }  //add
    }
}
