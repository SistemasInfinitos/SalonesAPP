using SalonesAPI.ModelsAPI;
using SalonesAPI.ModelsAPI.DataTable;
using SalonesAPI.ModelsAPI.Persona;
using System.Threading.Tasks;

namespace SalonesAPI.Repositorio.PersonasES
{
    public interface IPersonasESRepositorio
    {

        /// <summary>
        /// Actualiza una persona existente en la base de datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        Task<bool> ActualizarPersona(Personas entidad);
        /// <summary>
        /// Crea una nueva persona en la base de datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        Task<bool> CrearPersona(Personas entidad);

        /// <summary>
        /// trae una persona segun si id de la base de datos
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Personas> GetPersona(int Id);

        /// <summary>
        /// Trae las personas de la base de datos,  segun el rango de paginado configurado
        /// </summary>
        /// <returns></returns>
        Task<DataTableResponse> GetPersonasDataTable(DataTableParameter dtParameters);
    }
}
