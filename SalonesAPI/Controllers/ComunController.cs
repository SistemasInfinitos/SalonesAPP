using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComunController : ControllerBase
    {
        [Route("[action]", Name = "GetTest")]
        [HttpGet]
        public  IActionResult GetTest()
        {
            string mensaje =  "Api en linea!";
            return  Ok(mensaje);
        }
    }
}
