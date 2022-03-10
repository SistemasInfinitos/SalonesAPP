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
            // se usa transaccion por estandar, pero ya que es una sola tabla afectada no nesecita 
            using (var DbTran = _context.Database.BeginTransaction())
            {
                try
                {
                    Salone actualizarRegistro = _context.Salones.Where(x => x.Id == entidad.id).FirstOrDefault();

                    if (actualizarRegistro != null)
                    {
                        actualizarRegistro.IdMotivo = entidad.idMotivo;
                        actualizarRegistro.IdPersonaCliente = entidad.idPersonaCliente;
                        actualizarRegistro.FechaEvento = entidad.fechaEvento;
                        actualizarRegistro.CantidadPersona = entidad.cantidadPersona;
                        actualizarRegistro.Observacion = entidad.observacion;
                        actualizarRegistro.Estado = true;
                        actualizarRegistro.FechaActualizacion = DateTime.Now;

                        _context.Entry(actualizarRegistro).State = EntityState.Modified;
                        ok = await _context.SaveChangesAsync() > 0;
                    }

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

        public async Task<bool> CrearSalon(Salones entidad)
        {
            bool ok = false;
            // se usa transaccion por estandar y escalabilidad, pero ya que es una sola tabla afectada no se nesecita 
            using (var DbTran = _context.Database.BeginTransaction())
            {
                try
                {
                    Salone actualizarRegistro = _context.Salones.Where(x => x.Id == entidad.id).FirstOrDefault();
                    Salone registro = new Salone();
                    if (actualizarRegistro != null)
                    {
                        registro.IdMotivo = entidad.idMotivo;
                        registro.IdPersonaCliente = entidad.idPersonaCliente;
                        registro.FechaEvento = entidad.fechaEvento;
                        registro.CantidadPersona = entidad.cantidadPersona;
                        registro.Observacion = entidad.observacion;
                        registro.Estado = true;
                        registro.FechaCreacion = DateTime.Now;
                        registro.FechaActualizacion = null;

                        _context.Salones.Add(registro);
                        ok = await _context.SaveChangesAsync() > 0;
                    }

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

        public async Task<Salones> GetSalon(int Id)
        {
            Salones resevas = new Salones();
            try
            {
                var data = _context.Salones.Where(d => d.Id == Id).FirstOrDefault();
                if (data != null)
                {
                    resevas.id = data.Id;
                    resevas.idPersonaCliente = data.IdPersonaCliente;
                    resevas.cantidadPersona = data.CantidadPersona;
                    resevas.idMotivo = data.IdMotivo;
                    resevas.observacion = data.Observacion;
                    resevas.estado = true;
                    resevas.fechaEvento = data.FechaEvento.ToString("yyyy/MM/dd", cultureFecha);
                    resevas.fechaCreacion = data.FechaCreacion.ToString("yyyy/MM/dd", cultureFecha);
                    resevas.fechaActualizacion = data.FechaActualizacion != null ? data.FechaActualizacion.Value.ToString("yyyy/MM/dd", cultureFecha) : "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.Run(() => resevas);
        }

        public Task<DataTableResponse> GetSalonesDataTable(DataTableParameter dtParameters)
        {
            throw new NotImplementedException();
        }
    }
}

