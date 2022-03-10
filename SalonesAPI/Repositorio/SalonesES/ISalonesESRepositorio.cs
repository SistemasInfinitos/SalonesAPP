using SalonesAPI.ModelsAPI;
using SalonesAPI.ModelsAPI.DataTable;
using System.Threading.Tasks;

namespace SalonesAPI.Repositorio.SalonesES
{
    public interface ISalonesESRepositorio
    {
        /// <summary>
        /// Actualiza un Salon existente en la base de datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        Task<bool> ActualizarSalon(Salones entidad);

        /// <summary>
        /// Crea un nuevo Salon en la base de datos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        Task<bool> CrearSalon(Salones entidad);
        
        /// <summary>
        /// Elimina una reserva o salon de la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> BorrarSalon(int id);

        /// <summary>
        /// trae un Salon segun si id de la base de datos
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Salones> GetSalon(int Id);

        /// <summary>
        /// Trae las personas de la base de datos,  segun el rango de paginado configurado
        /// </summary>
        /// <returns></returns>
        Task<DataTableResponse> GetSalonesDataTable(DataTableParameter dtParameters);
    }
}
