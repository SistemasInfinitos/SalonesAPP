using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesAPI.ModelsAPI.Comun
{
    public class DepartamentoModel
    {
        public int id { get; set; }
        public int idPais { get; set; }
        public string distritoDepartamento { get; set; }
        public int? codigoDian { get; set; }
    }
}
