using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SalonesAPI.Configuration;
using SalonesAPI.ModelsAPI.Comun;
using SalonesAPI.ModelsAPI.DataTable;
using SalonesAPI.ModelsAPI.DataTable.Persona;
using SalonesAPI.ModelsAPI.Persona;
using SalonesAPI.ModelsDB;
using SalonesAPI.Repositorio.PersonasES;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace SalonesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly CultureInfo culture = new CultureInfo("is-IS");
        private readonly CultureInfo cultureFecha = new CultureInfo("en-US");
        private readonly JwtConfiguracion _jwtConfig;

        private readonly IPersonasESRepositorio _repositoryPersonas;
        public PersonasController(IOptionsMonitor<JwtConfiguracion> optionsMonitor, Context context)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _repositoryPersonas = new PersonasESRepositorio(optionsMonitor, context);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> CreatePersona([FromBody] PersonasModel entidad)
        {
            ResponseApp data = new ResponseApp()
            {
                mensaje = "Ups!. Tu Solicitud No Pudo ser Procesada",
                ok = false
            };
            try
            {
                if (ModelState.IsValid)
                {
                    data.ok = await Task.Run(() => _repositoryPersonas.CrearPersona(entidad));
                    data.mensaje = "Transaccion exitosa!";
                }
                else
                {
                    foreach (var item in ModelState.Values)
                    {
                        if (item.Errors[0].ErrorMessage == "")
                        {
                            data.mensaje += item.Errors[0].Exception.Message + " ";
                        }
                        else
                        {
                            data.mensaje += item.Errors[0].ErrorMessage + " ";
                        }
                    }
                }
                return Ok(data);
            }
            catch (Exception x)
            {
                data.mensaje = "Ups!. Algo salio mal!. Error interno. " + x.HResult;
                return BadRequest(data);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> ActualizarPersona([FromBody] PersonasModel entidad)
        {
            ResponseApp data = new ResponseApp()
            {
                mensaje = "Ups!. Tu Solicitud No Pudo ser Procesada",
                ok = false
            };
            try
            {
                if (ModelState.IsValid)
                {
                    data.ok = await Task.Run(() => _repositoryPersonas.ActualizarPersona(entidad));
                    data.mensaje = "Transaccion exitosa!";
                }
                else
                {
                    foreach (var item in ModelState.Values)
                    {
                        if (item.Errors[0].ErrorMessage == "")
                        {
                            data.mensaje += item.Errors[0].Exception.Message + " ";
                        }
                        else
                        {
                            data.mensaje += item.Errors[0].ErrorMessage + " ";
                        }
                    }
                }
                return Ok(data);
            }
            catch (Exception x)
            {
                data.mensaje = "Ups!. Algo salio mal!. Error interno. " + x.HResult;
                return BadRequest(data);
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> BorrarPersona(int? id)
        {
            ResponseApp data = new ResponseApp()
            {
                mensaje = "Ups!. Tu Solicitud No Pudo ser Procesada",
                ok = false
            };

            if (id != null)
            {
                try
                {
                    data.ok = await Task.Run(() => _repositoryPersonas.DeletePersona(id.Value));
                    return Ok(data);
                }
                catch (Exception x)
                {
                    data.mensaje = "Ups!. Algo salio mal!. Error interno. " + x.HResult;
                }
            }
            return BadRequest(data);
        }

        [Route("[action]", Name = "GetPersona")]
        [HttpGet]
        public async Task<IActionResult> GetPersona(string buscar, int? id)
        {
            bool ok = false;
            string mensaje = "Sin Datos";
            var persona = await Task.Run(() => _repositoryPersonas.GetPersona(buscar, id));
            if (persona != null && persona.id > 0)
            {
                mensaje = "ok";
                ok = true;
            }
            var data = new { persona, mensaje, ok };
            return Ok(data);
        }

        /// <summary>
        /// Metodo post espesial para Datatable
        /// </summary>
        /// <param name="dtParms"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ListPersonas([FromBody] DataTableParameter datatParms)
        {
            DataTableResponsePersona res = await Task.Run(() => _repositoryPersonas.GetPersonasDataTable(datatParms));
            res.draw = datatParms.draw;

            var json = JsonConvert.SerializeObject(res);

            return Ok(json);
        }
    }
}
