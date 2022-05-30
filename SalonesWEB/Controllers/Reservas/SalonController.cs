using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SalonesWEB.Configuration;
using SalonesWEB.Models.Comun;
using SalonesWEB.Models.Reservas;
using System;
using System.Collections.Generic;
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
        
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult> GetSalones()
        {
            return View();
        }
        public async Task<ActionResult> Gestion(string id)
        {
            // esta es una forma de trabajar, aumenta la seguridad pero tanbien el tiempo de desarrollo
            var httpClient = new HttpClient();
            string api = _jwtConfig.api; // esto garantiza la migracion a produccion ya que la url siempre cambia
            SalonesModel model = new SalonesModel();
            List<MotivosModel> modelMotivos = new List<MotivosModel>();
            List<DropListModel> modelPerona = new List<DropListModel>();

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
            #region Motivo
            ViewBag.idMotivo = new SelectList(modelMotivos, "id", "rangoEdades");
            string uriMotivos = api + "/api/Salones/GetMotivosReserva";
            var edades = await httpClient.GetAsync(uriMotivos);
            if (edades.IsSuccessStatusCode)
            {
                modelMotivos = JsonConvert.DeserializeObject<List<MotivosModel>>(await edades.Content.ReadAsStringAsync());
                ViewBag.idMotivo = new SelectList(modelMotivos, "id", "motivo", model.idMotivo);
            }
            #endregion
            #region Cliente
            ViewBag.idPersonaCliente = new SelectList(modelPerona, "id", "rangoEdades");
            int param = model.idPersonaCliente;
            //se estable la parsona para qu no traiga mas de uno ya que hay un buscar ajax dinamico
            string uriCliente = api + "/api/Personas/GetPersonasDropList" + "?id=" + param;
            var cliente = await httpClient.GetAsync(uriCliente);
            if (edades.IsSuccessStatusCode)
            {
                modelPerona = JsonConvert.DeserializeObject<List<DropListModel>>(await cliente.Content.ReadAsStringAsync());
                ViewBag.idPersonaCliente = new SelectList(modelPerona, "id", "text", model.idPersonaCliente);
            }
            #endregion
            #endregion
            return View(model);
        }
    }
}