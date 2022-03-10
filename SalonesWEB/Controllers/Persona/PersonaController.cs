using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        
        [Route("[action]")]
        public ActionResult Gestion()
        {
            var httpClient = new HttpClient();
            //string url = ConfigurationManager.AppSettings.Get("Service");
            string url = "";
            //var json = JsonConvert.SerializeObject(Object);
            //var data = new StringContent(json, Encoding.UTF8, "application/json");
            //var response =  httpClient.PostAsync(url, data);
            if (/*response.IsSuccessStatusCode*/ true)
            {
                //var dataObjects = response.Content.ReadAsAsync<object>().Result;
            }
                return View();
        }
    }
}
