using System;
using System.Collections.Generic;

#nullable disable

namespace SalonesAPI.ModelsDB
{
    public partial class Ciudade
    {
        public Ciudade()
        {
            Personas = new HashSet<Persona>();
        }

        public int Id { get; set; }
        public string CiudadNombre { get; set; }
        public int? IdDepartamento { get; set; }
        public int? CodigoDian { get; set; }

        public virtual Departamento IdDepartamentoNavigation { get; set; }
        public virtual ICollection<Persona> Personas { get; set; }
    }
}
