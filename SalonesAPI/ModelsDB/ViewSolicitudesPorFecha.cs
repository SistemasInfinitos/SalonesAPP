using System;
using System.Collections.Generic;

#nullable disable

namespace SalonesAPI.ModelsDB
{
    public partial class ViewSolicitudesPorFecha
    {
        public int id { get; set; }
        public DateTime fechaEvento { get; set; }
        public string fechaEventoTex { get; set; }
        public bool estado { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string correo { get; set; }
        public int edad { get; set; }
        public string identificacion { get; set; }
        public string telefono { get; set; }
        public int cantidadPersona { get; set; }
        public string observacion { get; set; }
        public string motivo { get; set; }
        public string ciudadNombre { get; set; }
        public string DistritoDepartamento { get; set; }
        public string paisNombre { get; set; }
    }
}
