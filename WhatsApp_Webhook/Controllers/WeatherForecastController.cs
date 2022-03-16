using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPS.Models;
using NPS.Services;

namespace WhatsApp_Webhook.Controllers
{
  
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly INPSRespository _respository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, INPSRespository respository)
        {
            _logger = logger;
            _respository = respository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }


       
        [HttpPost]
        public Pl_NPS_Aws PostNPS(Pl_NPS_Aws pl_new_nps)
        {
            Pl_NPS_Aws nps = new Pl_NPS_Aws();

            nps = _respository.Add_NPS_AWS(pl_new_nps);

            return nps;
        }


    }
}
