using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SalonesAPI.Configuration;
using SalonesAPI.Repositorio.SalonesES;
using System.Globalization;

namespace SalonesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonesController : ControllerBase
    {
        private readonly CultureInfo culture = new CultureInfo("is-IS");
        private readonly CultureInfo cultureFecha = new CultureInfo("en-US");
        private readonly JwtConfiguracion _jwtConfig;

        private readonly ISalonesESRepositorio _repositorySalones;
        public SalonesController(IOptionsMonitor<JwtConfiguracion> optionsMonitor)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            //_repositorySalones = new SalonesESRepositorio(optionsMonitor);
        }
    }
}
