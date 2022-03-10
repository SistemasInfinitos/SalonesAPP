using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesAPI.ModelsAPI.Persona
{
    public class EdadesModel
    {
        public int id { get; set; }
        public int edad { get; set; }
        public string Descripcion { get; set; }
        public string fechaCreacion { get; set; }
        public string fechaActualizacion { get; set; }
        public bool estado { get; set; }                  
    }
}
