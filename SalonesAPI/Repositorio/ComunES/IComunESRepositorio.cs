using SalonesAPI.ModelsAPI.Comun;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalonesAPI.Repositorio.ComunES
{
    public interface IComunESRepositorio
    {
        /// <summary>
        /// trae uno o todos los paises de la base de datos
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<List<PaisesModel>> GetPaises(string buscar, int? Id);

        /// <summary>
        /// trae uno o todos los departamentos de un pais
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<List<DepartamentoModel>> GetDepartamentos(string buscar, int? idPais);

        /// <summary>
        ///  trae uno o todas las ciudade de un departamento
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<List<CiudadesModel>> GetCiudades(string buscar, int? idDepartamento);
    }
}
