using Web.Template.Models;
using Web.Template.Models.ResponseModels;
using Web.Template.Services.Interfaces;

namespace Web.Template.Services
{
    public class WeatherForecastServices : IWeatherForecast
    {
        private readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public async Task<WebResponseModel> GetWeatherForecastAsync()
        {
            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return new WebResponseModel("Weather forecast data retrieved successfully",data);
        }
    }
}
