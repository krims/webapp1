using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1;
using WebApplication1.Controllers;
using WebApplication1.Database;
using WebApplication1.Models;
using WebApplication1.Service;
using Xunit;

namespace XUnitTestProject1
{
    // For xUnit you gotta do this setup fixture in order to do DI.
    // I saw folks complaining that you shouldn't do this for unit tests, just integration tests but oh well.
    public class DependencySetupFixture
    {
        public DependencySetupFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<WeatherForecastDbContext>(options => options.UseInMemoryDatabase(databaseName: "Forecasts"));
            serviceCollection.AddScoped<IWeatherForecastService, WeatherForecastService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
    public class UnitTest1 : IClassFixture<DependencySetupFixture>
    {
        //need this to get our dependency implementations
        private ServiceProvider _serviceProvider;

        public UnitTest1(DependencySetupFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        private static readonly string[] Summaries = new[]
{
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [Fact]
        public void Test1()
        {
            //set up our controller
            var controller = new WeatherForecastController(_serviceProvider.GetService<IWeatherForecastService>());

            //create a forecast
            // What is this random?
            var rng = new Random();
            var forecast = new WeatherForecastModel
            {
                Id = 1,
                Date = DateTime.Now,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            };

            //add to in mem db
            // how does this work?
            controller.addWeatherForecast(forecast);

            //make sure we get it back
            IEnumerable<WeatherForecastModel> forecasts = controller.GetWeatherForecasts();
            Assert.NotNull(forecasts);

            //Make sure we found Id=1 in the returned list
            var foundId = false;
            foreach (var cast in forecasts) {
                Assert.NotNull(cast);
                if (cast.Id == 1) foundId = true;
            }

            Assert.True(foundId);
        }
    }
}
