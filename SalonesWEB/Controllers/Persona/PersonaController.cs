using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SalonesWEB.Configuration;
using SalonesWEB.Models.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
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
            PersonasModel model = new PersonasModel();
            ViewBag.idString = "";
            if (!string.IsNullOrWhiteSpace(id))
            {
                ViewBag.idString = id;
                var httpClient = new HttpClient();
                string api = _jwtConfig.api;
                string endpoint = "api/Personas/GetPersona";
                string parmetro = "";
                string uri = api + "/" + endpoint + parmetro;

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
