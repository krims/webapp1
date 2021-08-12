using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class WeatherForecastService : IWeatherForecastService
    {
        //for this project we are just using an in memory DB defined by the DI from startup.cs
        private readonly WeatherForecastDbContext _db;

        public WeatherForecastService(WeatherForecastDbContext db)
        {
            _db = db;
        }

        public void addForecast(WeatherForecastModel forecast)
        {
            _db.Add(forecast);
            _db.SaveChanges();
        }

        public IEnumerable<WeatherForecastModel> getAllForecasts()
        {
            return _db.forecasts.ToList();
        }
    }
}
