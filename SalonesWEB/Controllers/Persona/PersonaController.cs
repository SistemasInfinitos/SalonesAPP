using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SalonesWEB.Configuration;
using SalonesWEB.Models.Comun;
using SalonesWEB.Models.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SalonesWEB.Controllers.Persona
{
    [Route("web/per")]
    public class PersonaController : Controller
    {
        private readonly JwtConfiguracion _jwtConfig;
        public PersonaController(IOptionsMonitor<JwtConfiguracion> optionsMonitor)
        {
            this._jwtConfig = optionsMonitor.CurrentValue;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult> Gestion(string id)
        {
            // esta es una forma de trabajar, aumenta la seguridad pero tanbien el tiempo de desarrollo
            var httpClient = new HttpClient();
            string api = _jwtConfig.api; // esto garantiza la migracion a produccion ya que la url siempre cambia
            PersonasModel model = new PersonasModel();
            List<EdadesModel> modelEdades = new List<EdadesModel>();
            List<CiudadesModel> modelCiudades = new List<CiudadesModel>();
            List<DepartamentoModel> modelDeparamentos = new List<DepartamentoModel>();
            List<PaisesModel> modelPaises = new List<PaisesModel>();
            ViewBag.idString = "";

            if (!string.IsNullOrWhiteSpace(id))
            {
                ViewBag.idString = id;
                string endpoint = "api/Personas/GetPersona";
                string parmetro = id;
                string uri = api + "/" + endpoint + "?id=" + parmetro;

                //var data = new StringContent("objt_json", Encoding.UTF8, "application/json");
                //var response =await  httpClient.PostAsync(uri, data);
                try
                {
                    var response = await httpClient.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<PersonasModel>(await response.Content.ReadAsStringAsync());
                    }
                }
                catch (Exception X)
                {
                    string mensaje = X.Message;
                    throw;
                }
            }
            //si entra en el anterio bloque SelectList seleccionara el actual
            #region DropDownList
            #region Edades
            ViewBag.edad = new SelectList(modelEdades, "id", "rangoEdades");
            string uriEdades = api + "/api/Comun/GetDropListRangoEades";
            var edades = await httpClient.GetAsync(uriEdades);
            if (edades.IsSuccessStatusCode)
            {
                modelEdades = JsonConvert.DeserializeObject<List<EdadesModel>>(await edades.Content.ReadAsStringAsync());
                ViewBag.edad = new SelectList(modelEdades, "id", "rangoEdades", model.edad);
            }
            #endregion

            #region Ciudades
            //ojo esto solo funciona para el caso de uso donde tengo muy pocas ciudades, de lo contrario seria una consuta muy costosa para el servidor
            // traer todos las ciudades de todos los deparatamentos y de todos los paises en un dropList no es una buena idea
            // sin embargo este mismo metodo-enpoint permite realizar consultas personalizables
            ViewBag.idCiudad = new SelectList(modelCiudades, "id", "ciudadNombre");
            string uriCiudades = api + "/api/Comun/GetDropListCiudades";
            var ciudades = await httpClient.GetAsync(uriCiudades);
            if (ciudades.IsSuccessStatusCode)
            {
                modelCiudades = JsonConvert.DeserializeObject<List<CiudadesModel>>(await ciudades.Content.ReadAsStringAsync());
                ViewBag.idCiudad = new SelectList(modelCiudades, "id", "ciudadNombre", model.idCiudad);
            }
            #endregion

            #region Departamentos
            //ojo esto solo funciona para el caso de uso donde tengo muy pocos departamentos, de lo contrario seria una consuta muy costosa para el servidor
            // traer todos los deperatementos de todos los paises en un dropList no es una buena idea
            // sin embargo este mismo metodo-enpoint permite realizar consultas personalizables
            ViewBag.departamento = new SelectList(modelDeparamentos, "id", "distritoDepartamento");
            
            if (model.idCiudad>0)//esto garantiza que no se carguen todos los departamentos en todos los casos
            {
                string uriDepartamentos = api + "/api/Comun/GetDropListDepartamentos";
                var deparamentos = await httpClient.GetAsync(uriDepartamentos);
                if (deparamentos.IsSuccessStatusCode)
                {
                    modelDeparamentos = JsonConvert.DeserializeObject<List<DepartamentoModel>>(await deparamentos.Content.ReadAsStringAsync());
                    ViewBag.departamento = new SelectList(modelDeparamentos, "id", "distritoDepartamento", modelCiudades.Where(x => x.id == model.idCiudad).Select(x => x.idDepartamento));
                }
            }
            #endregion

            #region Paises
            //ojo esto solo funciona para el caso de uso donde tengo muy pocos paises, de lo contrario seria una consuta muy costosa para el servidor
            // para garantiza una consulta limpia toca agregar pagunado inteligente en el enpoin-en el API
            // traer todos los paises del mundo en un dropList no es una buena idea
            // sin embargo este mismo metodo-enpoint permite realizar consultas personalizables
            ViewBag.pais = new SelectList(modelPaises, "id", "paisNombre");

            if (true)
            {
                string uriPaises = api + "/api/Comun/GetDropListPaises";
                var paises = await httpClient.GetAsync(uriPaises);
                if (paises.IsSuccessStatusCode)
                {
                    modelPaises = JsonConvert.DeserializeObject<List<PaisesModel>>(await paises.Content.ReadAsStringAsync());
                    ViewBag.pais = new SelectList(modelPaises, "id", "paisNombre", modelDeparamentos.Where(x=>x.id== modelCiudades.Where(s=>s.id==model.idCiudad).Select(n=>n.id).First()).Select(z=>z.idPais)); 
                }
            }
            #endregion

            #endregion
            return View(model);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult> GetPersonas()
        {
            return View();
        }
    }
}
