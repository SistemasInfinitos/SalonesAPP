namespace SalonesWEB.Models.Reservas
{
    public class SalonesModel
    {
        public int? id { get; set; }
        public int idPersonaCliente { get; set; }
        public string fechaEvento { get; set; }
        public int cantidadPersona { get; set; }
        public int idMotivo { get; set; }
        public string observacion { get; set; }
        public bool estado { get; set; }
        public string fechaCreacion { get; set; }
        public string fechaActualizacion { get; set; }
    }
}
