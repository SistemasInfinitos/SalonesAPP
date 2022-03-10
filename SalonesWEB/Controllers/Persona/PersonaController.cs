using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesWEB.Controllers.Persona
{
    public class PersonaController : Controller
    {
        // GET: PersonaController
        public ActionResult Gestion()
        {
            return View();
        }
    }
}
