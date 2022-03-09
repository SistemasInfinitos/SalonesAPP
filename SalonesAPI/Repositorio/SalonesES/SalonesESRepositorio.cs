using Microsoft.Extensions.Options;
using SalonesAPI.Configuration;
using SalonesAPI.ModelsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesAPI.Repositorio.SalonesES
{   
    public class SalonesESRepositorio:ISalonesESRepositorio
    {
        private readonly JwtConfig _jwtConfig;
        public SalonesESRepositorio(IOptionsMonitor<JwtConfig> optionsMonitor) 
        {
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        public Task<Salones> GetSalones(Salones entidad)
        {
            throw new NotImplementedException();
        }
    }
}

