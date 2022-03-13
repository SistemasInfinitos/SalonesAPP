namespace SalonesAPI.ModelsAPI.Reservas
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
        public virtual string fechaCreacion { get; set; }
        public virtual string fechaActualizacion { get; set; }
    }
}
