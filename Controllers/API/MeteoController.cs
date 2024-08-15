using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SensorApp.Data;
using SensorApp.DAO.MeteoDAO;
using System.Globalization;
using System.Collections.Generic;

namespace SensorApp.Controllers.API
{
    [Route("api/meteo")]
    [ApiController]
    public class MeteoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MeteoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{modo}/{fecha_desde}/{fecha_hasta}")]
        public async Task<IActionResult> GetMeteoData(string modo, DateTime? fecha_desde, DateTime? fecha_hasta)
        {
            if (!fecha_desde.HasValue || !fecha_hasta.HasValue)
            {
                return BadRequest("Both 'fecha_desde' and 'fecha_hasta' parameters are required.");
            }


            if (modo.ToLower() == "day")
            {
                var result = await GetDailyData(fecha_desde.Value, fecha_hasta.Value);
                return Ok(result);
            }
            else if (modo.ToLower() == "month")
            {
                var result = await GetMonthlyData(fecha_desde.Value, fecha_hasta.Value);
                return Ok(result);
            }
            else
            {
                return BadRequest($"Modo: { modo?.ToLower() } Invalid. Use 'day' or 'month'");
            }
        }

        private async Task<MeteoResponse> GetDailyData(DateTime fecha_desde, DateTime fecha_hasta)
        {
            var data = await _context.SensorData
                .Where(sd => sd.FechaDato >= fecha_desde && sd.FechaDato <= fecha_hasta.AddDays(1))
                .GroupBy(sd => new { sd.CodigoParametro, sd.NombreParametro, sd.FechaDato.Date })
                .Select(g => new
                {
                    g.Key.CodigoParametro,
                    g.Key.NombreParametro,
                    UnidadParametro = _context.Parameters.FirstOrDefault(p => p.CodigoParametro == g.Key.CodigoParametro).Unidad,
                    AbreviacionParametro = _context.Parameters.FirstOrDefault(p => p.CodigoParametro == g.Key.CodigoParametro).Abreviacion,
                    AvgData = g.Average(p => p.ValorNumero),
                    MinData = g.Min(p => p.ValorNumero),
                    MaxData = g.Max(p => p.ValorNumero),
                    FechaDato = g.Key.Date
                })
                .OrderBy(d => d.FechaDato)
                .ToListAsync();

            var deviceDates = data.Select(d => d.FechaDato.ToString("yyyy-MM-dd"))
                 .OrderBy(date => date)
                 .Distinct()
                 .ToList();

            var deviceData = data
                .GroupBy(d => new { d.CodigoParametro, d.NombreParametro, d.UnidadParametro, d.AbreviacionParametro })
                .Select(g => new DeviceData
                {
                    CodigoParametro = g.Key.CodigoParametro.ToString(),
                    NombreParametro = g.Key.NombreParametro.ToUpper(),
                    UnidadParametro = g.Key.UnidadParametro,
                    AbreviacionParametro = g.Key.AbreviacionParametro.ToUpper(),
                    Values = new Values
                    {
                        AvgData = g.Select(d => d.AvgData).ToList(),
                        MinData = g.Select(d => d.MinData).ToList(),
                        MaxData = g.Select(d => d.MaxData).ToList()
                    }
                })

                .ToList();

            return new MeteoResponse
            {
                DeviceDates = deviceDates,
                DeviceData = deviceData
            };
        }

        private async Task<MeteoResponse> GetMonthlyData(DateTime fecha_desde, DateTime fecha_hasta)
        {
            var data = await _context.SensorData
                .Where(sd => sd.FechaDato >= fecha_desde && sd.FechaDato <= fecha_hasta.AddDays(1))
                .GroupBy(sd => new { sd.CodigoParametro, sd.NombreParametro, MonthYear = new DateTime(sd.FechaDato.Year, sd.FechaDato.Month, 1) })
                .Select(g => new
                {
                    g.Key.CodigoParametro,
                    g.Key.NombreParametro,
                    UnidadParametro = _context.Parameters.FirstOrDefault(p => p.CodigoParametro == g.Key.CodigoParametro).Unidad,
                    AbreviacionParametro = _context.Parameters.FirstOrDefault(p => p.CodigoParametro == g.Key.CodigoParametro).Abreviacion,
                    AvgData = g.Average(x => x.ValorNumero),
                    MinData = g.Min(x => x.ValorNumero),
                    MaxData = g.Max(x => x.ValorNumero),
                    MonthYear = g.Key.MonthYear
                })
                .OrderBy(d => d.MonthYear)
                .ToListAsync();

            var deviceDates = data
                .Select(d => $"{d.MonthYear.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)} – {d.MonthYear.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}")
                .OrderBy(date => date)
                .Distinct()
                .ToList();

            var deviceData = data
                .GroupBy(d => new { d.CodigoParametro, d.NombreParametro, d.UnidadParametro, d.AbreviacionParametro })
                .Select(g => new DeviceData
                {
                    CodigoParametro = g.Key.CodigoParametro.ToString(),
                    NombreParametro = g.Key.NombreParametro.ToUpper(),
                    UnidadParametro = g.Key.UnidadParametro,
                    AbreviacionParametro = g.Key.AbreviacionParametro.ToUpper(),
                    Values = new Values
                    {
                        AvgData = g.Select(d => d.AvgData).ToList(),
                        MinData = g.Select(d => d.MinData).ToList(),
                        MaxData = g.Select(d => d.MaxData).ToList()
                    }
                })
                .ToList();

            return new MeteoResponse
            {
                DeviceDates = deviceDates,
                DeviceData = deviceData
            };
        }
    }
}