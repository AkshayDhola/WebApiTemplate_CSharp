using Web.Template.Models.ResponseModels;

namespace Web.Template.Services.Interfaces
{
    public interface IWeatherForecast
    {
        public Task<WebResponseModel> GetWeatherForecastAsync();
    }
}
