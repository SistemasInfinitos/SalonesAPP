using System;
using System.Collections.Generic;

#nullable disable

namespace SalonesAPI.ModelsDB
{
    public partial class Edade
    {
        public int id { get; set; }
        public int edad { get; set; }
        public string Descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaActualizacion { get; set; }
        public bool? estado { get; set; }
    }
}
