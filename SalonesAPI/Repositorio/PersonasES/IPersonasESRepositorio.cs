using SalonesAPI.ModelsAPI.DataTable;
using SalonesAPI.ModelsAPI.DataTable.Persona;
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
        Task<bool> ActualizarPersona(PersonasModel entidad);
        /// <summary>
        /// Crea una nueva persona en la base de datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        Task<bool> CrearPersona(PersonasModel entidad);

        /// <summary>
        /// trae una persona segun si id de la base de datos
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<PersonasModel> GetPersona(string buscar,int? Id);

        /// <summary>
        /// Trae las personas de la base de datos,  segun el rango de paginado configurado
        /// </summary>
        /// <returns></returns>
        Task<DataTableResponsePersona> GetPersonasDataTable(DataTableParameter dtParameters);
    }
}
