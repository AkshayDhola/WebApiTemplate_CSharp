namespace Web.Template.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Web.Template.Services.Interfaces;
    
    [Route("v1")]
    public class WeatherForecastController : AppController
    {
        private readonly IWeatherForecast _weatherForecast;

        public WeatherForecastController(IWeatherForecast weatherForecast)
        {
            _weatherForecast = weatherForecast;
        }

        [HttpGet("WeatherForecast")]
        public async Task<IActionResult> GetWeatherForecast()
        {
            return await HandleRequestAsync(() => _weatherForecast.GetWeatherForecastAsync());
        }
    }
}
