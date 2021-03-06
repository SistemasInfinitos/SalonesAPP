using Microsoft.Extensions.Options;
using SalonesAPI.Configuration;
using SalonesAPI.ModelsAPI.Comun;
using SalonesAPI.ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesAPI.Repositorio.ComunES
{
    public class ComunESRepositorio : IComunESRepositorio
    {
        private readonly JwtConfiguracion _jwtConfig;
        private readonly Context _context;

        public ComunESRepositorio(IOptionsMonitor<JwtConfiguracion> optionsMonitor, Context context)
        {
            //_jwtConfig = optionsMonitor.CurrentValue;
            _context = context;
        }

        public async Task<List<CiudadesModel>> GetCiudades(string buscar, int? idDepartamento)
        {
            List<CiudadesModel> data = new List<CiudadesModel>();
            try
            {
                var predicado = PredicateBuilder.True<Ciudade>();
                var predicado2 = PredicateBuilder.False<Ciudade>();

                if (!string.IsNullOrWhiteSpace(buscar))
                {
                    predicado2 = predicado2.Or(d => 1 == 1 && d.ciudadNombre.Contains(buscar));

                    predicado = predicado.And(predicado2);
                }
                if (idDepartamento != null)
                {
                    predicado = predicado.And(d => d.idDepartamento == idDepartamento);
                }
                var datos2 = _context.Ciudades.Where(predicado).ToList();
                data = datos2.Select(x => new CiudadesModel
                {
                    id = x.id,
                    ciudadNombre = x.ciudadNombre,
                    idDepartamento = x.idDepartamento,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.Run(() => data);
        }

        public async Task<List<DepartamentoModel>> GetDepartamentos(string buscar, int? idPais)
        {
            List<DepartamentoModel> data = new List<DepartamentoModel>();
            try
            {
                var predicado = PredicateBuilder.True<Departamento>();
                var predicado2 = PredicateBuilder.False<Departamento>();

                if (!string.IsNullOrWhiteSpace(buscar))
                {
                    predicado2 = predicado2.Or(d => 1 == 1 && d.DistritoDepartamento.Contains(buscar));

                    predicado = predicado.And(predicado2);
                }
                if (idPais != null)
                {
                    predicado = predicado.And(d => d.IdPais == idPais);
                }
                var datos2 = _context.Departamentos.Where(predicado).ToList();
                data = datos2.Select(x => new DepartamentoModel
                {
                    id = x.Id,
                    distritoDepartamento = x.DistritoDepartamento,
                    idPais = x.IdPais
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.Run(() => data);
        }

        public async Task<List<PaisesModel>> GetPaises(string buscar, int? Id)
        {
            List<PaisesModel> data = new List<PaisesModel>();
            try
            {
                var predicado = PredicateBuilder.True<Paise>();
                var predicado2 = PredicateBuilder.False<Paise>();

                if (!string.IsNullOrWhiteSpace(buscar))
                {
                    predicado2 = predicado2.Or(d => 1 == 1 && d.paisNombre.Contains(buscar));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.paisCodigo.Contains(buscar));
                    predicado2 = predicado2.Or(d => 1 == 1 && d.paisContinente.Contains(buscar));

                    predicado = predicado.And(predicado2);
                }
                if (Id != null)
                {
                    predicado = predicado.And(d => d.id == Id);
                }
                var datos2 = _context.Paises.Where(predicado).ToList();
                data = datos2.Select(x => new PaisesModel
                {
                    id = x.id,
                    paisNombre = x.paisNombre,
                    paisCodigo = x.paisCodigo,
                    paisContinente = x.paisContinente,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.Run(() => data);
        }
    }
}
