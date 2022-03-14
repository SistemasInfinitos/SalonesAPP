using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SalonesWEB.Configuration;
using SalonesWEB.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace SalonesWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly JwtConfiguracion _jwtConfig;

        public HomeController(ILogger<HomeController> logger, IOptionsMonitor<JwtConfiguracion> optionsMonitor)
        {
            _logger = logger;
            this._jwtConfig = optionsMonitor.CurrentValue;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<JsonResult> GetService()
        {
            bool ok = false;
            string mensaje = "recurso inaccesible ";
            string api = _jwtConfig.api;
            var httpClient = new HttpClient();
            string endpoint = "api/Comun/GetTest";
            string uri = api + "/" + endpoint;
            try
            {
                var response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    ok = true;
                    mensaje = "Conectado!";
                }
            }
            catch (Exception X)
            {
                mensaje += X.Message;
            }
            var data = new { api, ok, mensaje };
            return Json(data);
        }

    }
}
