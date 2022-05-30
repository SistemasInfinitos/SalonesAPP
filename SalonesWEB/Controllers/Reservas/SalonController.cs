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
    }
}
