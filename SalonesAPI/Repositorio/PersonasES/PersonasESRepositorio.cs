using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SalonesAPI.Configuration;
using SalonesAPI.ModelsAPI.Comun;
using SalonesAPI.ModelsAPI.DataTable;
using SalonesAPI.ModelsAPI.DataTable.Persona;
using SalonesAPI.ModelsAPI.Persona;
using SalonesAPI.ModelsDB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesAPI.Repositorio.PersonasES
{
    public class PersonasESRepositorio : IPersonasESRepositorio
    {
        private readonly JwtConfiguracion _jwtConfig;
        private readonly Context _context;

        public PersonasESRepositorio(IOptionsMonitor<JwtConfiguracion> optionsMonitor, Context context)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _context = context;
        }

        private readonly CultureInfo culture = new CultureInfo("is-IS");
        private readonly CultureInfo cultureFecha = new CultureInfo("en-US");

        public async Task<bool> ActualizarPersona(PersonasModel entidad)
        {
            bool ok = false;
            using (var DbTran = _context.Database.BeginTransaction())
            {
                try
                {
                    Persona actualizarRegistro = _context.Personas.Where(x => x.id == entidad.id).FirstOrDefault();

                    if (actualizarRegistro != null)
                    {
                        actualizarRegistro.primerNombre = entidad.primerNombre;
                        actualizarRegistro.segundoNombre = entidad.segundoNombre;
                        actualizarRegistro.primerApellido = entidad.primerApellido;
                        actualizarRegistro.segundoApellido = entidad.segundoApellido;
                        actualizarRegistro.telefono = entidad.telefono;
                        actualizarRegistro.identificacion = entidad.identificacion;
                        actualizarRegistro.edad = entidad.edad.Value;
                        actualizarRegistro.correo = entidad.correo;
                        actualizarRegistro.idCiudad = entidad.idCiudad.Value;
                        actualizarRegistro.estado = true;
                        actualizarRegistro.fechaCreacion = DateTime.Now;
                        actualizarRegistro.fechaActualizacion = null;

                        actualizarRegistro.fechaActualizacion = DateTime.Now;

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

        public async Task<bool> CrearPersona(PersonasModel entidad)
        {
            bool ok = false;

            using (var DbTran = _context.Database.BeginTransaction())
            {
                try
                {
                    var verificarExiste = _context.Personas.Where(x => x.identificacion == entidad.identificacion).FirstOrDefault();
                    Persona nuevoRegistro = new Persona();
                    if (verificarExiste == null)
                    {
                        nuevoRegistro.primerNombre = entidad.primerNombre;
                        nuevoRegistro.segundoNombre = entidad.segundoNombre;
                        nuevoRegistro.primerApellido = entidad.primerApellido;
                        nuevoRegistro.segundoApellido = entidad.segundoApellido;
                        nuevoRegistro.telefono = entidad.telefono;
                        nuevoRegistro.identificacion = entidad.identificacion;
                        nuevoRegistro.edad = entidad.edad.Value;
                        nuevoRegistro.correo = entidad.correo;
                        nuevoRegistro.idCiudad = entidad.idCiudad.Value;
                        nuevoRegistro.estado = true;
                        nuevoRegistro.fechaCreacion = DateTime.Now;
                        nuevoRegistro.fechaActualizacion = null;

                        _context.Personas.Add(nuevoRegistro);
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

        public async Task<PersonasModel> GetPersona(string buscar, int? Id)
        {
            PersonasModel persona = new PersonasModel();
            try
            {
                var predicado = PredicateBuilder.True<Persona>();
                var predicado2 = PredicateBuilder.False<Persona>();
                predicado = predicado.And(d => d.estado == true);

                if (!string.IsNullOrWhiteSpace(buscar))
                {
                    predicado2 = predicado2.Or(d => 1 == 1 && d.primerNombre.Contains(buscar));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.segundoNombre.Contains(buscar));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.primerApellido.Contains(buscar));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.segundoNombre.Contains(buscar));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.correo.Contains(buscar));
                    predicado = predicado.And(predicado2);
                }
                if (Id != null)
                {
                    predicado = predicado.And(x => x.id == Id);
                }
                var data = _context.Personas.Where(predicado).FirstOrDefault();
                if (data != null)
                {
                    persona.id = data.id;
                    persona.primerNombre = data.primerNombre;
                    persona.segundoNombre = data.segundoNombre;
                    persona.primerApellido = data.primerApellido;
                    persona.segundoApellido = data.segundoApellido;
                    persona.telefono = data.telefono;
                    persona.identificacion = data.identificacion;
                    persona.edad = data.edad;
                    persona.correo = data.correo;
                    persona.idCiudad = data.idCiudad;
                    persona.estado = true;
                    persona.fechaCreacion = data.fechaCreacion.ToString("yyyy/MM/dd", cultureFecha);
                    persona.fechaActualizacion = data.fechaActualizacion != null ? data.fechaActualizacion.Value.ToString("yyyy/MM/dd", cultureFecha) : "";
                    persona.cliente = data.primerNombre + (!string.IsNullOrWhiteSpace(data.segundoNombre) ? " " + data.segundoNombre : "") + " " + data.primerApellido + "" + (!string.IsNullOrWhiteSpace(data.segundoApellido) ? " " + data.segundoApellido : "");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.Run(() => persona);
        }

        public async Task<DataTableResponsePersona> GetPersonasDataTable(DataTableParameter dtParameters)
        {
            try
            {
                DataTableResponsePersona datos = new DataTableResponsePersona();
                string search = dtParameters.search?.value;
                search = search.Replace(" ", "");
                List<string> sortcolumn2 = new List<string>();
                string sortcolumn3 = "";

                if (dtParameters != null && dtParameters.order != null && dtParameters.order.Count() > 0)
                {
                    foreach (var id in dtParameters.order)
                    {
                        sortcolumn2.Add(dtParameters.columns[id.column].name);
                        sortcolumn3 += (dtParameters.columns[id.column].name) + ",";
                    }
                }
                string sortcolumn = dtParameters.columns[dtParameters.order[0].column].name;

                var predicado = PredicateBuilder.True<Persona>();
                var predicado2 = PredicateBuilder.False<Persona>();
                predicado = predicado.And(d => d.estado == true);

                if (!string.IsNullOrWhiteSpace(dtParameters.search.value))
                {
                    predicado2 = predicado2.Or(d => 1 == 1 && d.primerNombre.Contains(dtParameters.search.value));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.segundoNombre.Contains(dtParameters.search.value));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.primerApellido.Contains(dtParameters.search.value));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.segundoNombre.Contains(dtParameters.search.value));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.correo.Contains(dtParameters.search.value));
                    predicado = predicado.And(predicado2);
                }

                datos.recordsFiltered = _context.Personas.Where(predicado).ToList().Count();
                datos.recordsTotal = datos.recordsFiltered;
                datos.draw = dtParameters.draw;

                if (dtParameters.length == -1)
                {
                    dtParameters.length = datos.recordsFiltered;
                }
                string order = "asc";
                if (dtParameters.order.Count >0)
                {
                    order = dtParameters.order?[0].dir;
                }
                if (string.IsNullOrWhiteSpace(sortcolumn))
                {
                    sortcolumn = "PrimerNombre";
                }
                List<Persona> datos2 = new List<Persona>();
                if (datos.recordsFiltered > 0)
                {
                    datos2 = _context.Personas.Where(predicado).OrderBy2(sortcolumn, order).Skip(dtParameters.start).Take(dtParameters.length).ToList();
                    datos.data = datos2.Select(x => new PersonasModel
                    {
                        id = x.id,
                        primerNombre = x.primerNombre,
                        primerApellido = x.primerApellido,
                        segundoNombre = x.segundoNombre,
                        segundoApellido = x.segundoApellido,
                        telefono = x.telefono,
                        correo = x.correo,
                        edad = x.edad,
                        estado = x.estado.Value,
                        identificacion = x.identificacion,
                        idCiudad = x.idCiudad,
                        fechaActualizacion = x.fechaActualizacion != null ? x.fechaActualizacion.Value.ToString("yyyy/MM/dd", culture) : "",
                        fechaCreacion = x.fechaCreacion.ToString("yyyy/MM/dd", culture),

                    }).ToList();
                }


                return await Task.Run(() => datos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeletePersona(int id)
        {
            bool ok = false;
            using (var DbTran = _context.Database.BeginTransaction())
            {
                try
                {
                    var delete = _context.Personas.Where(x => x.id == id).FirstOrDefault();

                    if (delete != null)
                    {
                        _context.Entry(delete).State = EntityState.Deleted;
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

        public async Task<List<DropListModel>> GetPersonasDropList(string buscar, int? id)
        {
            List<DropListModel> datos = new List<DropListModel>();
            try
            {
                var predicado = PredicateBuilder.True<Persona>();
                var predicado2 = PredicateBuilder.False<Persona>();
                predicado = predicado.And(d => d.estado == true);

                if (!string.IsNullOrWhiteSpace(buscar))
                {
                    predicado2 = predicado2.Or(d => 1 == 1 && d.primerNombre.Contains(buscar));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.segundoNombre.Contains(buscar));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.primerApellido.Contains(buscar));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.segundoNombre.Contains(buscar));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.correo.Contains(buscar));
                    predicado = predicado.And(predicado2);
                }
                if (id != null)
                {
                    predicado = predicado.And(d => d.id == id);
                }

                var data = _context.Personas.Where(predicado).Take(10).ToList();

                datos = data.Select(x => new DropListModel
                {
                    id = x.id,
                    text = x.primerNombre + (!string.IsNullOrWhiteSpace(x.segundoNombre) ? " " + x.segundoNombre : "") + " " + x.primerApellido + "" + (!string.IsNullOrWhiteSpace(x.segundoApellido) ? " " + x.segundoApellido : "")
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.Run(() => datos);
        }
    }
}