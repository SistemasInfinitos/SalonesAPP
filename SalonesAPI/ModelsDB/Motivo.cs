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

        public int Id { get; set; }
        public string Motivo1 { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Salone> Salones { get; set; }
    }
}
