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
                    Persona actualizarRegistro = _context.Personas.Where(x => x.Id == entidad.id).FirstOrDefault();

                    if (actualizarRegistro != null)
                    {
                        actualizarRegistro.PrimerNombre = entidad.primerNombre;
                        actualizarRegistro.SegundoNombre = entidad.segundoNombre;
                        actualizarRegistro.PrimerApellido = entidad.primerApellido;
                        actualizarRegistro.SegundoApellido = entidad.segundoApellido;
                        actualizarRegistro.Telefono = entidad.telefono;
                        actualizarRegistro.Identificacion = entidad.identificacion;
                        actualizarRegistro.Edad = entidad.edad;
                        actualizarRegistro.Correo = entidad.correo;
                        actualizarRegistro.IdCiudad = entidad.idCiudad;
                        actualizarRegistro.Estado = true;
                        actualizarRegistro.FechaCreacion = DateTime.Now;
                        actualizarRegistro.FechaActualizacion = null;

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

        public async Task<bool> CrearPersona(PersonasModel entidad)
        {
            bool ok = false;

            using (var DbTran = _context.Database.BeginTransaction())
            {
                try
                {
                    var verificarExiste = _context.Personas.Where(x => x.Identificacion == entidad.identificacion).FirstOrDefault();
                    Persona nuevoRegistro = new Persona();
                    if (verificarExiste == null)
                    {
                        nuevoRegistro.PrimerNombre = entidad.primerNombre;
                        nuevoRegistro.SegundoNombre = entidad.segundoNombre;
                        nuevoRegistro.PrimerApellido = entidad.primerApellido;
                        nuevoRegistro.SegundoApellido = entidad.segundoApellido;
                        nuevoRegistro.Telefono = entidad.telefono;
                        nuevoRegistro.Identificacion = entidad.identificacion;
                        nuevoRegistro.Edad = entidad.edad;
                        nuevoRegistro.Correo = entidad.correo;
                        nuevoRegistro.IdCiudad = entidad.idCiudad;
                        nuevoRegistro.Estado = true;
                        nuevoRegistro.FechaCreacion = DateTime.Now;
                        nuevoRegistro.FechaActualizacion = null;

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
                predicado = predicado.And(d => d.Estado == true);

                if (!string.IsNullOrWhiteSpace(buscar))
                {
                    predicado2 = predicado2.Or(d => 1 == 1 && d.PrimerNombre.Contains(buscar));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.SegundoNombre.Contains(buscar));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.PrimerApellido.Contains(buscar));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.SegundoNombre.Contains(buscar));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.Correo.Contains(buscar));
                    predicado = predicado.And(predicado2);
                }
                if (Id != null)
                {
                    predicado = predicado.And(x => x.Id == Id);
                }
                var data = _context.Personas.Where(predicado).FirstOrDefault();
                if (data != null)
                {
                    persona.id = data.Id;
                    persona.primerNombre = data.PrimerNombre;
                    persona.segundoNombre = data.SegundoNombre;
                    persona.primerApellido = data.PrimerApellido;
                    persona.segundoApellido = data.SegundoApellido;
                    persona.telefono = data.Telefono;
                    persona.identificacion = data.Identificacion;
                    persona.edad = data.Edad;
                    persona.correo = data.Correo;
                    persona.idCiudad = data.IdCiudad;
                    persona.estado = true;
                    persona.fechaCreacion = data.FechaCreacion.ToString("yyyy/MM/dd", cultureFecha);
                    persona.fechaActualizacion = data.FechaActualizacion != null ? data.FechaActualizacion.Value.ToString("yyyy/MM/dd", cultureFecha) : "";
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
                predicado = predicado.And(d => d.Estado == true);

                if (!string.IsNullOrWhiteSpace(dtParameters.search.value))
                {
                    predicado2 = predicado2.Or(d => 1 == 1 && d.PrimerNombre.Contains(dtParameters.search.value));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.SegundoNombre.Contains(dtParameters.search.value));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.PrimerApellido.Contains(dtParameters.search.value));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.SegundoNombre.Contains(dtParameters.search.value));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.Correo.Contains(dtParameters.search.value));
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
                if (dtParameters.order.Count > 1)
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
                        id = x.Id,
                        primerNombre = x.PrimerNombre,
                        primerApellido = x.PrimerApellido,
                        segundoNombre = x.SegundoNombre,
                        segundoApellido = x.SegundoApellido,
                        telefono = x.Telefono,
                        correo = x.Correo,
                        edad = x.Edad,
                        estado = x.Estado.Value,
                        identificacion = x.Identificacion,
                        idCiudad = x.IdCiudad,
                        fechaActualizacion = x.FechaActualizacion != null ? x.FechaActualizacion.Value.ToString("yyyy/MM/dd", culture) : "",
                        fechaCreacion = x.FechaCreacion.ToString("yyyy/MM/dd", culture),

                    }).ToList();
                }


                return await Task.Run(() => datos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> DeletePersona(int id)
        {
            throw new NotImplementedException();
        }
    }
}