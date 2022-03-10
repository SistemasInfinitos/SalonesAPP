﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesAPI.ModelsAPI.Comun
{
    public class PaisesModel
    {
        public string paisCodigo { get; set; }
        public string paisNombre { get; set; }
        public string paisContinente { get; set; }
        public int id { get; set; }
        public int? codigoDian { get; set; }
    }
}
