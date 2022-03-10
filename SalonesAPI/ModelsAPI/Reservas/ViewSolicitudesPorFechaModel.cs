using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesAPI.ModelsAPI.Reservas
{
    public class ViewSolicitudesPorFechaModel
    {
        public int Id { get; set; }
        public DateTime FechaEvento { get; set; }
        public string FechaEventoTex { get; set; }
        public bool Estado { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Correo { get; set; }
        public int Edad { get; set; }
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
        public int CantidadPersona { get; set; }
        public string Observacion { get; set; }
        public string Motivo { get; set; }
        public string CiudadNombre { get; set; }
        public string DistritoDepartamento { get; set; }
        public string PaisNombre { get; set; }
    }
}
