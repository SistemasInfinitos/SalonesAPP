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

        public int id { get; set; }
        public string ciudadNombre { get; set; }
        public int? idDepartamento { get; set; }
        public int? codigoDian { get; set; }

        public virtual Departamento idDepartamentoNavigation { get; set; }
        public virtual ICollection<Persona> Personas { get; set; }
    }
}
