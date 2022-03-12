using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SalonesAPI.Configuration;
using SalonesAPI.ModelsAPI.Comun;
using SalonesAPI.ModelsDB;
using SalonesAPI.Repositorio.ComunES;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesAPI.Controllers
{
    //[EnableCors("AudienciaPolicy")]
    //[Authorize(Policy = "AudienciaPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComunController : ControllerBase
    {
        private readonly JwtConfiguracion _jwtConfig;
        private readonly IComunESRepositorio _repositoryComun;
        public ComunController(IOptionsMonitor<JwtConfiguracion> optionsMonitor, Context context)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _repositoryComun = new ComunESRepositorio(optionsMonitor, context);
        }

        [Route("[action]", Name = "GetTest")]
        [HttpGet]
        public IActionResult GetTest()
        {
            string mensaje = "Api en linea!";
            return Ok(mensaje);
        }


        //[EnableCors("AudienciaPolicy")]
        //[Authorize(Policy = "AudienciaPolicy")]
        [Route("[action]", Name = "GetDropListPaises")]
        [HttpGet]
        public async Task<IActionResult> GetDropListPaises(string buscar, int? id)
        {
            bool ok = false;
            string mensaje = "Sin Datos";
            var paises = await Task.Run(() => _repositoryComun.GetPaises(buscar, id));
            if (paises != null && paises.Count() > 0)
            {
                mensaje = "ok";
                ok = true;
            }
            //var data = new { paises, mensaje, ok };
            var json = JsonConvert.SerializeObject(paises);
            return Ok(json);
        }

        [Route("[action]", Name = "GetDropListDepartamentos")]
        [HttpGet]
        public async Task<IActionResult> GetDropListDepartamentos(string buscar, int? idPais)
        {
            bool ok = false;
            string mensaje = "Sin Datos";
            var dapartamentos = await Task.Run(() => _repositoryComun.GetDepartamentos(buscar, idPais));
            if (dapartamentos != null && dapartamentos.Count() > 0)
            {
                mensaje = "ok";
                ok = true;
            }
            //var data = new { dapartamentos, mensaje, ok };
            var json = JsonConvert.SerializeObject(dapartamentos);
            return Ok(json);
        }


        [Route("[action]", Name = "GetDropListCiudades")]
        [HttpGet]
        public async Task<IActionResult> GetDropListCiudades(string buscar, int? idDepartamento)
        {
            bool ok = false;
            string mensaje = "Sin Datos";
            var ciudades = await Task.Run(() => _repositoryComun.GetCiudades(buscar, idDepartamento));
            if (ciudades != null && ciudades.Count() > 0)
            {
                mensaje = "ok";
                ok = true;
            }
            //var data = new { ciudades, mensaje, ok };// si lo hago asi complicaria mas la deserializacion si se consume desde un controller mvc
            var json = JsonConvert.SerializeObject(ciudades);
            return Ok(json);
        }


        [Route("[action]", Name = "GetDropListRangoEades")]
        [HttpGet]
        public async Task<IActionResult> GetDropListRangoEades()
        {
            bool ok = true;
            string mensaje = "ok";
            List<EdadesModel> edades = new List<EdadesModel>();

            // como no se define claramente si se requiere la lista de posible edad de una persona -- considero que deberia ser una lista de rango de edades
            //esto podria almacener en base de datros igual que las ciudades pero por temas de tiempo lo realizo desde aqui
            edades.Add(new EdadesModel { id = 1, rangoEdades = "1-15" });
            edades.Add(new EdadesModel { id = 2, rangoEdades = "16-17" });
            edades.Add(new EdadesModel { id = 3, rangoEdades = "18-24" });
            edades.Add(new EdadesModel { id = 4, rangoEdades = "25-30" });
            edades.Add(new EdadesModel { id = 5, rangoEdades = "31-40" });

            var listEdades = await Task.Run(() => edades);

            //var data = new { listEdades, mensaje, ok };// si lo hago asi complicaria mas la deserializacion si se consume desde un controller mvc
            var json = JsonConvert.SerializeObject(listEdades);
            return Ok(json);
        }
    }
}
