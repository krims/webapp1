using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IWeatherForecastService _service;

        public WeatherForecastController(IWeatherForecastService service)
        {
            _service = service;
        }

        [HttpPost]
        public void addWeatherForecast(WeatherForecastModel forecast)
        {
            _service.addForecast(forecast);
        }

        [HttpGet]
        public IEnumerable<WeatherForecastModel> GetWeatherForecasts()
        {
            return _service.getAllForecasts();
        }
    }
}
