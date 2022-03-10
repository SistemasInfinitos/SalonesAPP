﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SalonesAPI.Configuration;
using SalonesAPI.ModelsAPI;
using SalonesAPI.ModelsAPI.Comun;
using SalonesAPI.ModelsDB;
using SalonesAPI.Repositorio.PersonasES;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace SalonesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly CultureInfo culture = new CultureInfo("is-IS");
        private readonly CultureInfo cultureFecha = new CultureInfo("en-US");
        private readonly JwtConfiguracion _jwtConfig;

        private readonly IPersonasESRepositorio _repositoryPersonas;
        public PersonasController(IOptionsMonitor<JwtConfiguracion> optionsMonitor, Context context)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _repositoryPersonas = new PersonasESRepositorio(optionsMonitor, context);
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> CreatePersona([FromBody] Personas entidad)
        {
            ResponseApp data = new ResponseApp()
            {
                mensaje = "Ups!. Tu Solicitud No Pudo ser Procesada",
                ok = false
            };
            try
            {
                if (ModelState.IsValid)
                {
                    data.ok= await Task.Run(() => _repositoryPersonas.CrearPersona(entidad));
                }
                else
                {
                    foreach (var item in ModelState.Values)
                    {
                        if (item.Errors[0].ErrorMessage == "")
                        {
                            data.mensaje += item.Errors[0].Exception.Message + " ";
                        }
                        else
                        {
                            data.mensaje += item.Errors[0].ErrorMessage + " ";
                        }
                    }
                }
                return Ok(data);
            }
            catch (Exception x)
            {
                data.mensaje = "Ups!. Algo salio mal!. Error interno. " + x.HResult;
                return BadRequest(data);
            }
        }


        [Route("[action]", Name = "GetPersona")]
        [HttpGet]
        public async Task<IActionResult> GetPersona(int Id)
        {
            bool ok = false;
            string mensaje = "Sin Datos";
            var data = await Task.Run(() => _repositoryPersonas.GetPersona(Id));
            if (data != null)
            {
                mensaje = "ok";
                ok = true;
            }
            return Ok(new { data, ok, mensaje });
        }
    }
}
