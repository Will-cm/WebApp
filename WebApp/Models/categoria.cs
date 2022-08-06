using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class categoria
    {
        public int id { get; set; }
        public string codigo { get; set; } = null!;
        public string? descripcion { get; set; }
        public short? estado { get; set; }
        public virtual ICollection<subcategoria>? subcategoria { get; set; }  //add
        //public List<subcategoria> subcategoria { get; set; }  //add
    }
}
