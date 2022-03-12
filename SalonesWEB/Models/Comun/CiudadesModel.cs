using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesWEB.Models.Comun
{
    public class CiudadesModel
    {
        public int id { get; set; }
        public string ciudadNombre { get; set; }
        public int? idDepartamento { get; set; }
        public int? codigoDian { get; set; }
    }
}
