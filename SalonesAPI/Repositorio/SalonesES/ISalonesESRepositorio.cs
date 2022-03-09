using SalonesAPI.ModelsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesAPI.Repositorio.SalonesES
{
    public interface ISalonesESRepositorio
    {
        Task<Salones> GetSalones(Salones entidad);
    }
}
