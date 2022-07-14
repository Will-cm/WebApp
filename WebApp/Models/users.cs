using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class users
    {
        public int id { get; set; }
        public string nombre { get; set; } = null!;
        public string apellido { get; set; } = null!;
        public string? ci { get; set; }
        public string? telefono { get; set; }
        public string? direccion { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public short? estado { get; set; }
        public int? id_rol { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
