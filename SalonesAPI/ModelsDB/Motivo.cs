using System;
using System.Collections.Generic;

#nullable disable

namespace SalonesAPI.ModelsDB
{
    public partial class Motivo
    {
        public Motivo()
        {
            Salones = new HashSet<Salone>();
        }

        public int id { get; set; }
        public string motivo1 { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaActualizacion { get; set; }
        public bool? estado { get; set; }

        public virtual ICollection<Salone> Salones { get; set; }
    }
}
