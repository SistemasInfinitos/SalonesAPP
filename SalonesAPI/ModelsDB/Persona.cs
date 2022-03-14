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

        public int id { get; set; }
        public string identificacion { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public int idCiudad { get; set; }
        public int edad { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaActualizacion { get; set; }
        public bool? estado { get; set; }

        public virtual Ciudade idCiudadNavigation { get; set; }
        public virtual ICollection<Salone> Salones { get; set; }
    }
}
