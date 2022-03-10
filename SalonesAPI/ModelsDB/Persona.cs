using System;

#nullable disable

namespace SalonesAPI.ModelsDB
{
    public partial class Persona
    {
        public int Id { get; set; }
        public string Identificacion { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public int IdCiudad { get; set; }
        public int Edad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public bool? Estado { get; set; }
    }
}
