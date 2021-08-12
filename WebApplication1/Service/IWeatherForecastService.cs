using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public interface IWeatherForecastService
    {

        public void addForecast(WeatherForecastModel forecast);
        public IEnumerable<WeatherForecastModel> getAllForecasts();

    }
}
