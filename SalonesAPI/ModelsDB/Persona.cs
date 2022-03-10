using System;
using System.Collections.Generic;

#nullable disable

namespace SalonesAPI.ModelsDB
{
    public partial class Persona
    {
        public Persona()
        {
            Salones = new HashSet<Salone>();
        }

        public int Id { get; set; }
        public string Identificacion { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public int IdCiudad { get; set; }
        public int Edad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public bool? Estado { get; set; }

        public virtual Ciudade IdCiudadNavigation { get; set; }
        public virtual ICollection<Salone> Salones { get; set; }
    }
}
