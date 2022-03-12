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

        public string PaisCodigo { get; set; }
        public string PaisNombre { get; set; }
        public string PaisContinente { get; set; }
        public int Id { get; set; }
        public int? CodigoDian { get; set; }

        public virtual ICollection<Departamento> Departamentos { get; set; }
    }
}
