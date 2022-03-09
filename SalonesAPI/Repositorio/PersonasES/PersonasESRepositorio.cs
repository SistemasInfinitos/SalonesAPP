using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SalonesAPI.Configuration;
using SalonesAPI.ModelsAPI;
using SalonesAPI.ModelsAPI.Comun;
using SalonesAPI.ModelsAPI.DataTable;
using SalonesAPI.ModelsDB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesAPI.Repositorio.PersonasES
{
    public class PersonasESRepositorio: IPersonasESRepositorio
    {
        private readonly JwtConfiguracion _jwtConfig;
        private readonly Context _context;

        public PersonasESRepositorio(IOptionsMonitor<JwtConfiguracion> optionsMonitor, Context db)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _context = db;
        }

        private readonly CultureInfo culture = new CultureInfo("is-IS");
        private readonly CultureInfo cultureFecha = new CultureInfo("en-US");

        public Task<bool> ActualizarPersona(Personas entidad)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CrearPersona(Personas entidad)
        {
            throw new NotImplementedException();
        }

        public Task<Personas> GetPersona(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<DataTableResponse> GetPersonasDataTable(DataTableParameter dtParameters)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> ActualizarPersona(Personas entidad)
        //{
        //    bool ok = false;
        //    using (var DbTran = _context.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            Personas ActualizarRegistro = _context.Personas.Where(x => x.Id == entidad.id).FirstOrDefault();

        //            if (ActualizarRegistro != null)
        //            {
        //                ActualizarRegistro.primerNombre = entidad.primerNombre;
        //                ActualizarRegistro.segundoNombre = entidad.segundoNombre;
        //                ActualizarRegistro.primerApellido = entidad.primerApellido;
        //                ActualizarRegistro.segundoApellido = entidad.segundoApellido;
        //                ActualizarRegistro.telefono = entidad.telefono;
        //                ActualizarRegistro.identificacion = entidad.identificacion;
        //                ActualizarRegistro.correo = entidad.correo;
        //                ActualizarRegistro.estado = entidad.estado;

        //                ActualizarRegistro.fechaActualizacion = DateTime.Now;

        //                _context.Entry(ActualizarRegistro).State = EntityState.Modified;
        //                ok =await _context.SaveChangesAsync() > 0;
        //            }

        //            if (ok)
        //            {
        //                DbTran.Commit();
        //            }
        //        }
        //        catch (Exception x)
        //        {
        //            DbTran.Rollback();
        //        }
        //    }
        //    return await Task.Run(() => ok);
        //}

        //public async Task<bool> CrearPersona(Personas entidad)
        //{
        //    bool ok = false;

        //    using (var DbTran = _context.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var verificarExiste = _context.Personas.Where(x => x.ide == entidad.estado).FirstOrDefault();
        //            Personas nuevoRegistro = new Personas();
        //            if (verificarExiste == null)
        //            {
        //                nuevoRegistro.primerNombre = entidad.primerNombre;
        //                nuevoRegistro.segundoNombre = entidad.segundoNombre;
        //                nuevoRegistro.primerApellido = entidad.primerApellido;
        //                nuevoRegistro.segundoApellido = entidad.segundoApellido;
        //                nuevoRegistro.telefono = entidad.telefono;
        //                nuevoRegistro.identificacion = entidad.identificacion;
        //                nuevoRegistro.correo = entidad.correo;
        //                nuevoRegistro.estado = entidad.estado;

        //                _context.Personas.Add(nuevoRegistro);
        //                ok = await _context.SaveChangesAsync() > 0;
        //            }

        //            if (ok)
        //            {
        //                DbTran.Commit();
        //            }
        //        }
        //        catch (Exception x)
        //        {
        //            DbTran.Rollback();
        //        }
        //    }
        //    return await Task.Run(() => ok);
        //}


        //public async Task<Personas> GetPersona(int Id)
        //{
        //    try
        //    {
        //        var x = _context.Personas.Where(d => d.Id == Id).FirstOrDefault();
        //        var Persona = new Personas
        //        {
        //            id = x.Id,
        //            primerNombre = x.PrimerNombre,
        //            primerApellido = x.PrimerApellido,
        //            segundoNombre = x.SegundoNombre,
        //            segundoApellido = x.SegundoApellido,
        //            identificacion = x.NumeroIdentificacion,
        //            fechaCreacion = x.FechaNacimiento.Value.ToString("yyyy/MM/dd", cultureFecha),
        //        };
        //        return await Task.Run(() => Persona);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<DataTableResponse> GetPersonasDataTable(DataTableParameter dtParameters)
        //{
        //    try
        //    {
        //        DataTableResponse datos = new DataTableResponse();
        //        string search = dtParameters.search.value;
        //        search = search.Replace(" ", "");
        //        List<string> sortcolumn2 = new List<string>();
        //        string sortcolumn3 = "";

        //        foreach (var id in dtParameters.order)
        //        {
        //            sortcolumn2.Add(dtParameters.columns[id.column].name);
        //            sortcolumn3 += (dtParameters.columns[id.column].name) + ",";
        //        }
        //        string sortcolumn = dtParameters.columns[dtParameters.order[0].column].name;
        //        string sortcolumn1 = sortcolumn;
        //        if (dtParameters.order.Count > 1)
        //        {
        //            sortcolumn1 = dtParameters.columns[dtParameters.order[1].column].name;
        //        }

        //        var predicado = PredicateBuilder.True<Personas>();
        //        var predicado2 = PredicateBuilder.False<Personas>();
        //        predicado = predicado.And(d => d.estado == true );

        //        if (!string.IsNullOrWhiteSpace(dtParameters.search.value))
        //        {
        //            predicado2 = predicado2.Or(d => 1 == 1 && d.primerNombre.Contains(dtParameters.search.value));
        //            predicado2 = predicado2.Or(d => 1 == 1 && d.segundoNombre.Contains(dtParameters.search.value));
        //            predicado2 = predicado2.Or(d => 1 == 1 && d.primerApellido.Contains(dtParameters.search.value));
        //            predicado2 = predicado2.Or(d => 1 == 1 && d.segundoNombre.Contains(dtParameters.search.value));
        //            predicado2 = predicado2.Or(d => 1 == 1 && d.correo.Contains(dtParameters.search.value));
        //            predicado = predicado.And(predicado2);
        //        }

        //        datos.recordsFiltered = _context.Personas.Where(predicado).ToList().Count();
        //        datos.recordsTotal = datos.recordsFiltered;
        //        datos.draw = dtParameters.draw;

        //        if (dtParameters.length == -1)
        //        {
        //            dtParameters.length = datos.recordsFiltered;
        //        }
        //        string order = "asc";
        //        string order2 = "asc";

        //        order = dtParameters.order[0].dir;
        //        if (dtParameters.order.Count > 1)
        //        {
        //            order2 = dtParameters.order[1].dir;
        //        }

        //        var datos2 = _context.Personas.Where(predicado).OrderBy2(sortcolumn, order).ThenBy2(sortcolumn1, order2).Skip(dtParameters.start).Take(dtParameters.length).ToList();
        //        datos.data = datos2.Select(x => new Personas
        //        {
        //            primerNombre = x.PrimerNombre,
        //            primerApellido = x.PrimerApellido,
        //            segundoNombre = x.SegundoNombre,
        //            segundoApellido = x.SegundoApellido,

        //            fechaActualizacion = x.fechaActualizacion != null ? x.fechaActualizacion.Value.ToString("yyyy/MM/dd", culture) : "",
        //            fechaCreacion = x.fechaCreacion.ToString("yyyy/MM/dd", culture),

        //        }).ToList();

        //        return await Task.Run(() => datos);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}