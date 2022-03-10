using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SalonesAPI.Configuration;
using SalonesAPI.ModelsAPI;
using SalonesAPI.ModelsAPI.DataTable;
using SalonesAPI.ModelsDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;

namespace SalonesAPI.Repositorio.SalonesES
{
    public class SalonesESRepositorio : ISalonesESRepositorio
    {
        private readonly JwtConfiguracion _jwtConfig;
        private readonly Context _context;

        public SalonesESRepositorio(IOptionsMonitor<JwtConfiguracion> optionsMonitor, Context context)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _context = context;
        }

        private readonly CultureInfo culture = new CultureInfo("is-IS");
        private readonly CultureInfo cultureFecha = new CultureInfo("en-US");

        public Task<bool> ActualizarSalon(Salones entidad)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> BorrarSalon(int id)
        {           
            bool ok = false;

            try
            {
                string sp = "SpDeleteReserva";
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter() { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.Int });

                ok = await _context.Database.ExecuteSqlRawAsync(sp, parametros)>0;
            }
            catch (Exception e)
            {
                string debug = e.Message;
                throw;
            }
            return await Task.Run(() => ok);
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

