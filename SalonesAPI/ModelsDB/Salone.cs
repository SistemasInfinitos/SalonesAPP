using System;
using System.Collections.Generic;

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

        public virtual Motivo IdMotivoNavigation { get; set; }
        public virtual Persona IdPersonaClienteNavigation { get; set; }
    }
}
