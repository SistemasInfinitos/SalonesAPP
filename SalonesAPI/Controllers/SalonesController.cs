using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SalonesAPI.Configuration;
using SalonesAPI.ModelsAPI.Comun;
using SalonesAPI.ModelsAPI.DataTable;
using SalonesAPI.ModelsAPI.Reservas;
using SalonesAPI.ModelsDB;
using SalonesAPI.Repositorio.SalonesES;
using System;
using System.Threading.Tasks;

namespace SalonesAPI.Controllers
{
    //[EnableCors("AudienciaPolicy")]
    //[Authorize(Policy = "AudienciaPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class SalonesController : ControllerBase
    {
        private readonly ISalonesESRepositorio _repositorySaloness;
        public SalonesController(IOptionsMonitor<JwtConfiguracion> optionsMonitor, Context context)
        {
            _repositorySaloness = new SalonesESRepositorio(optionsMonitor, context);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> CreateReservaSalon([FromBody] SalonesModel entidad)
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
                    data.ok = await Task.Run(() => _repositorySaloness.CrearSalon(entidad));
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
        [HttpPost]
        public async Task<IActionResult> BorrarReservaSalon(int? id)
        {
            ResponseApp data = new ResponseApp()
            {
                mensaje = "Ups!. Tu Solicitud No Pudo ser Procesada",
                ok = false
            };
            try
            {
                if (id!=null)
                {
                    data.ok = await Task.Run(() => _repositorySaloness.BorrarSalon(id.Value));
                    data.mensaje = "Transaccion exitosa!";
                }
                else
                {
                    data.mensaje ="Ups!. Algo salio mal! debes validar el id enviado!";
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
        public async Task<IActionResult> ActualizarReservaSalon([FromBody] SalonesModel entidad)
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
                    data.ok = await Task.Run(() => _repositorySaloness.ActualizarSalon(entidad));
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

        [Route("[action]", Name = "GetSalon")]
        [HttpGet]
        public async Task<IActionResult> GetSalon(int Id)
        {
            bool ok = false;
            string mensaje = "Sin Datos";
            var data = await Task.Run(() => _repositorySaloness.GetSalon(Id));
            if (data != null)
            {
                mensaje = "ok";
                ok = true;
            }
            //return Ok(new { data, ok, mensaje });
            return Ok(data);
        }

        [Route("[action]", Name = "GetMotivosReserva")]
        [HttpGet]
        public async Task<IActionResult> GetMotivosReserva(string buscar)
        {
            bool ok = false;
            string mensaje = "Sin Datos";
            var data = await Task.Run(() => _repositorySaloness.GetMotivos(buscar));
            if (data != null)
            {
                mensaje = "ok";
                ok = true;
            }
            //return Ok(new { data, ok, mensaje });
            return Ok(data);
        }

        /// <summary>
        /// Metodo post espesial para Datatable
        /// </summary>
        /// <param name="dtParms"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ReporteGestionSalones(DataTableParameter dtParms)
        {
            DataTableViewSolicitudesPorFechaModel res = await Task.Run(() => _repositorySaloness.GetSalonesDataTable(dtParms));
            res.draw = dtParms.draw;

            var json = JsonConvert.SerializeObject(res);

            return Ok(json);
        }
    }
}
