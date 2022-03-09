using System;

namespace SalonesAPI.ModelsAPI
{
    public class Salones
    {
        public int idPersonaCliente { get; set; }
        public DateTime fechaEvento { get; set; }
        public int cantidadPersona { get; set; }
        public int idMotivo { get; set; }
        public string observacion { get; set; }
        public bool estado { get; set; }
    }
}
