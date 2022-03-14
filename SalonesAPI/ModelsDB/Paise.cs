using System;
using System.Collections.Generic;

#nullable disable

namespace SalonesAPI.ModelsDB
{
    public partial class Paise
    {
        public Paise()
        {
            Departamentos = new HashSet<Departamento>();
        }

        public string paisCodigo { get; set; }
        public string paisNombre { get; set; }
        public string paisContinente { get; set; }
        public int id { get; set; }
        public int? codigoDian { get; set; }

        public virtual ICollection<Departamento> Departamentos { get; set; }
    }
}
