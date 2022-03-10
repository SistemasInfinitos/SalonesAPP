using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SalonesAPI.Configuration;
using SalonesAPI.ModelsAPI;
using SalonesAPI.ModelsAPI.DataTable;
using SalonesAPI.ModelsAPI.Reservas;
using SalonesAPI.ModelsDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
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

        public async Task<bool> ActualizarSalon(Salones entidad)
        {
            bool ok = false;
            using (var DbTran = _context.Database.BeginTransaction())
            {
                try
                {
                    Salone actualizarRegistro = _context.Salones.Where(x => x.Id == entidad.id).FirstOrDefault();

                    //if (actualizarRegistro != null)
                    //{
                    //    actualizarRegistro.IdMotivo = entidad.primerNombre;
                    //    actualizarRegistro.IdPersonaCliente = entidad.segundoNombre;
                    //    actualizarRegistro.FechaEvento = entidad.primerApellido;
                    //    actualizarRegistro.SegundoApellido = entidad.segundoApellido;
                    //    actualizarRegistro.Telefono = entidad.telefono;
                    //    actualizarRegistro.Identificacion = entidad.identificacion;
                    //    actualizarRegistro.Edad = entidad.edad;
                    //    actualizarRegistro.Correo = entidad.correo;
                    //    actualizarRegistro.IdCiudad = entidad.idCiudad;
                    //    actualizarRegistro.Estado = true;
                    //    actualizarRegistro.FechaCreacion = DateTime.Now;
                    //    actualizarRegistro.FechaActualizacion = null;

                    //    actualizarRegistro.FechaActualizacion = DateTime.Now;

                    //    _context.Entry(actualizarRegistro).State = EntityState.Modified;
                    //    ok = await _context.SaveChangesAsync() > 0;
                    //}

                    if (ok)
                    {
                        DbTran.Commit();
                    }
                }
                catch (Exception x)
                {
                    DbTran.Rollback();
                }
            }
            return await Task.Run(() => ok);
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

