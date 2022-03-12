using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SalonesWEB.Configuration;
using SalonesWEB.Models.Reservas;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SalonesWEB.Controllers.Reservas
{
    [Route("web/Res")]
    public class SalonController : Controller
    {
        private readonly JwtConfiguracion _jwtConfig;
        public SalonController(IOptionsMonitor<JwtConfiguracion> optionsMonitor)
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
            SalonesModel model = new SalonesModel();

            ViewBag.idString = "";

            if (!string.IsNullOrWhiteSpace(id))
            {
                ViewBag.idString = id;
                string endpoint = "api/Salones/GetSalon";
                string parmetro = id;
                string uri = api + "/" + endpoint + "?id=" + parmetro;

                try
                {
                    var response = await httpClient.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<SalonesModel>(await response.Content.ReadAsStringAsync());
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
            #endregion
            return View(model);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult> GetSalones()
        {
            return View();
        }
    }
}
