using System;
using System.Collections.Generic;

#nullable disable

namespace SalonesAPI.ModelsDB
{
    public partial class Salone
    {
        public int id { get; set; }
        public int idPersonaCliente { get; set; }
        public DateTime fechaEvento { get; set; }
        public int cantidadPersona { get; set; }
        public int idMotivo { get; set; }
        public string observacion { get; set; }
        public bool? estado { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaActualizacion { get; set; }

        public virtual Motivo idMotivoNavigation { get; set; }
        public virtual Persona idPersonaClienteNavigation { get; set; }
    }
}
