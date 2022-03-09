using SalonesAPI.ModelsAPI;
using System.Threading.Tasks;

namespace SalonesAPI.Repositorio
{
    public interface ISalonesESRepositorio
    {
        Task<Salones> GetSalones(Salones entidad);
    }
}
