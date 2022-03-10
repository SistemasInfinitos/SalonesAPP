using System.Collections.Generic;

#nullable disable

namespace SalonesAPI.ModelsDB
{
    public partial class Departamento
    {
        public Departamento()
        {
            Ciudades = new HashSet<Ciudade>();
        }

        public int Id { get; set; }
        public int IdPais { get; set; }
        public string DistritoDepartamento { get; set; }
        public int? CodigoDian { get; set; }

        public virtual Paise IdPaisNavigation { get; set; }
        public virtual ICollection<Ciudade> Ciudades { get; set; }
    }
}
