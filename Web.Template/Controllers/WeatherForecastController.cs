using Microsoft.AspNetCore.Mvc;
using Web.Template.Models.ResponseModels;
using Web.Template.Services.Interfaces;

namespace Web.Template.Controllers
{
    [Route("v1")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecast _weatherForecast;

        public WeatherForecastController(IWeatherForecast weatherForecast)
        {
            _weatherForecast = weatherForecast;
        }

        [HttpGet("WeatherForecast")]
        public async Task<IActionResult> GetWeatherForecast()
        {
            WebResponseModel response = await _weatherForecast.GetWeatherForecastAsync();
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
