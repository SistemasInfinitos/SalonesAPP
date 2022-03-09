using Microsoft.Extensions.Options;
using SalonesAPI.Configuration;
using SalonesAPI.ModelsAPI;
using SalonesAPI.ModelsAPI.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesAPI.Repositorio.SalonesES
{   
    public class SalonesESRepositorio:ISalonesESRepositorio
    {
        private readonly JwtConfiguracion _jwtConfig;
        public SalonesESRepositorio(IOptionsMonitor<JwtConfiguracion> optionsMonitor) 
        {
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        public Task<bool> ActualizarSalon(Salones entidad)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CrearSalon(Salones entidad)
        {
            throw new NotImplementedException();
        }

        public Task<Salones> GetSalon(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<DataTableResponse> GetSalonesDataTable(DataTableParameter dtParameters)
        {
            throw new NotImplementedException();
        }
    }
}

