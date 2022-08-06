using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class subcategoria
    {
        public int id { get; set; }
        public string codigo { get; set; } = null!;
        public string? descripcion { get; set; }
        public short? estado { get; set; }
        public int? cod_categoria { get ; set; }
        public virtual categoria? categoria { get; set; } //add
        //public virtual ICollection<categoria> categoria { get; set; }
    }
}
