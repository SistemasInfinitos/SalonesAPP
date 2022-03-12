using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SalonesAPI.Configuration;
using SalonesAPI.ModelsAPI.Comun;
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

        public async Task<bool> ActualizarSalon(SalonesModel entidad)
        {
            bool ok = false;
            // se usa transaccion por estandar, pero ya que es una sola tabla afectada no nesecita 
            using (var DbTran = _context.Database.BeginTransaction())
            {
                try
                {
                    Salone actualizarRegistro = _context.Salones.Where(x => x.Id == entidad.id).FirstOrDefault();
                    var convertir = DateTime.TryParseExact(entidad.fechaEvento, "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out DateTime fecha);

                    if (actualizarRegistro != null && convertir)
                    {
                        actualizarRegistro.IdMotivo = entidad.idMotivo;
                        actualizarRegistro.IdPersonaCliente = entidad.idPersonaCliente;
                        actualizarRegistro.FechaEvento = fecha;
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

                ok = await _context.Database.ExecuteSqlRawAsync(sp, parametros) > 0;
            }
            catch (Exception e)
            {
                string debug = e.Message;
                throw;
            }
            return await Task.Run(() => ok);
        }

        public async Task<bool> CrearSalon(SalonesModel entidad)
        {
            bool ok = false;
            // se usa transaccion por estandar y escalabilidad, pero ya que es una sola tabla afectada no se nesecita 
            using (var DbTran = _context.Database.BeginTransaction())
            {
                try
                {
                    //var convertirA = DateTime.TryParse(entidad.fechaEvento, DateTimeStyles.None, cultureFecha, out DateTime fechaZZ);
                    var convertir = DateTime.TryParseExact(entidad.fechaEvento, "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out DateTime fecha);
                    Salone actualizarRegistro = _context.Salones.Where(x => x.Id == entidad.id).FirstOrDefault();
                    Salone registro = new Salone();
                    if (actualizarRegistro != null && convertir)
                    {
                        registro.IdMotivo = entidad.idMotivo;
                        registro.IdPersonaCliente = entidad.idPersonaCliente;
                        registro.FechaEvento = fecha;
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

        public async Task<SalonesModel> GetSalon(int Id)
        {
            SalonesModel resevas = new SalonesModel();
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

        public async Task<DataTableViewSolicitudesPorFechaModel> GetSalonesDataTable(DataTableParameter dtParameters)
        {
            try
            {
                DataTableViewSolicitudesPorFechaModel datos = new DataTableViewSolicitudesPorFechaModel();
                string search = dtParameters.search?.value;
                search = search?.Replace(" ", "");
                List<string> sortcolumn2 = new List<string>();
                string sortcolumn3 = "";

                if (dtParameters != null && dtParameters.order != null && dtParameters.order.Count() > 0)
                {
                    foreach (var id in dtParameters?.order)
                    {
                        sortcolumn2.Add(dtParameters.columns[id.column].name);
                        sortcolumn3 += (dtParameters.columns[id.column].name) + ",";
                    }
                }
                string sortcolumn = dtParameters.columns != null && dtParameters.order != null && dtParameters.order[0].column != null ?
                    dtParameters.columns[dtParameters.order[0].column].name : "";
                string sortcolumn1 = sortcolumn;
                if (dtParameters.order != null && dtParameters.order.Count > 1)
                {
                    sortcolumn1 = dtParameters.columns[dtParameters.order[1].column].name;
                }

                var predicado = PredicateBuilder.True<ViewSolicitudesPorFecha>();
                var predicado2 = PredicateBuilder.False<ViewSolicitudesPorFecha>();
                predicado = predicado.And(d => d.Estado == true);

                if (!string.IsNullOrWhiteSpace(dtParameters.search?.value))
                {
                    predicado2 = predicado2.Or(d => 1 == 1 && d.PrimerNombre.Contains(dtParameters.search.value));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.SegundoNombre.Contains(dtParameters.search.value));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.PrimerApellido.Contains(dtParameters.search.value));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.SegundoNombre.Contains(dtParameters.search.value));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.Correo.Contains(dtParameters.search.value));
                    predicado = predicado.And(predicado2);
                }

                //if (!string.IsNullOrWhiteSpace(dtParameters.fechaDesde))
                //{
                //    DateTime fechax = DateTime.Now;
                //    bool convertir = DateTime.TryParse(dtParameters.fechaDesde, out fechax);
                //    if (convertir)
                //    {
                //        predicado = predicado.And(d => d.FechaEvento >= fechax);
                //    }
                //}

                //if (!string.IsNullOrWhiteSpace(dtParameters.fechaHasta))
                //{
                //    DateTime fechax2 = DateTime.Now;
                //    bool convertir = DateTime.TryParse(dtParameters.fechaHasta, out fechax2);
                //    if (convertir)
                //    {
                //        fechax2 = fechax2.AddDays(1);
                //        predicado = predicado.And(d => d.FechaEvento <= fechax2);
                //    }
                //}

                datos.recordsFiltered = _context.ViewSolicitudesPorFechas.Where(predicado).ToList().Count();
                datos.recordsTotal = datos.recordsFiltered;
                if (dtParameters.start == null)
                {
                    dtParameters.start = 0;
                }
                datos.draw = dtParameters.draw;

                if (dtParameters.length == -1)
                {
                    dtParameters.length = datos.recordsFiltered;
                }
                string order = "asc";

                if (dtParameters.order?.Count > 1)
                {
                    order = dtParameters.order?[0].dir;
                }
                if (string.IsNullOrWhiteSpace(sortcolumn))
                {
                    sortcolumn = "PrimerNombre";
                }
                var datos2 = _context.ViewSolicitudesPorFechas.Where(predicado).OrderBy2(sortcolumn, order).Skip((dtParameters.start)).Take((dtParameters.length)).ToList();
                datos.data = datos2.Select(x => new ViewSolicitudesPorFechaModel
                {
                    id = x.Id,
                    fechaEvento = x.FechaEvento,
                    fechaEventoTex = x.FechaEventoTex,
                    estado = x.Estado,
                    primerNombre = x.PrimerNombre,
                    segundoNombre = x.SegundoApellido,
                    primerApellido = x.PrimerApellido,
                    segundoApellido = x.SegundoApellido,
                    correo = x.Correo,
                    edad = x.Edad,
                    identificacion = x.Identificacion,
                    telefono = x.Telefono,
                    cantidadPersona = x.CantidadPersona,
                    observacion = x.Observacion,
                    motivo = x.Motivo,
                    ciudadNombre = x.CiudadNombre,
                    distritoDepartamento = x.DistritoDepartamento,
                    paisNombre = x.PaisNombre

                }).ToList();

                return await Task.Run(() => datos);
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public async Task<List<MotivosModel>> GetMotivos(string buscar)
        {
            List<MotivosModel> motivos = new List<MotivosModel>();
            try
            {
                var predicado = PredicateBuilder.True<Motivo>();
                var predicado2 = PredicateBuilder.False<Motivo>();
                predicado = predicado.And(d => d.Estado == true);

                if (!string.IsNullOrWhiteSpace(buscar))
                {
                    predicado2 = predicado2.Or(d => 1 == 1 && d.Motivo1.Contains(buscar));

                    predicado = predicado.And(predicado2);
                }

                var datos2 = _context.Motivos.Where(predicado).ToList();
                motivos = datos2.Select(x => new MotivosModel
                {
                    id = x.Id,
                    motivo = x.Motivo1,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.Run(() => motivos);
        }
    }
}

