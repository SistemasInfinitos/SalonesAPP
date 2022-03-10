﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesAPI.ModelsAPI.Reservas
{
    public class MotivosModel
    {
        public int id { get; set; }
        public string motivo { get; set; }
        public string fechaCreacion { get; set; }
        public string fechaActualizacion { get; set; }
        public bool estado { get; set; }    
    }
}
