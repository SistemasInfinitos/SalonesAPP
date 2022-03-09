using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesAPI.Configuration
{
    public class JwtConfig
    {
        public string cadenaConeccion { get; set; }
        public string[] audience { get; set; }
    }
}
