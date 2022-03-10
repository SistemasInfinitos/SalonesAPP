using System;

#nullable disable

namespace SalonesAPI.ModelsDB
{
    public partial class Salone
    {
        public int Id { get; set; }
        public int IdPersonaCliente { get; set; }
        public DateTime FechaEvento { get; set; }
        public int CantidadPersona { get; set; }
        public int IdMotivo { get; set; }
        public string Observacion { get; set; }
        public bool? Estado { get; set; }
    }
}
