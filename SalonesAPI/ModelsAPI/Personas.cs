using System;
using System.ComponentModel.DataAnnotations;
namespace SalonesAPI.ModelsAPI
{
    public class Personas
    {
        public int id { get; set; }
        [Required]
        public string identificacion { get; set; }
        [Required]
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        [Required]
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        [Required]
        public string telefono { get; set; }
        [Required]
        public string correo { get; set; }
        [Required]
        public int idDepartamento { get; set; }
        [Required]
        public int idCiudad { get; set; }
        [Required]
        public int edad { get; set; }
        public DateTime fechaCreacion { get; set; }

        //[BsonRepresentation(BsonType.DateTime)]
        public DateTime? fechaActualizacion { get; set; }
        public bool estado { get; set; }
    }
}
