namespace Web.Template.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Web.Template.Models.ResponseModels;

    public class AppController : ControllerBase
    {
        protected async Task<IActionResult> HandleRequestAsync(Func<Task<WebResponseModel>> action)
        {
            try
            {
                var response = await action();
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                var exception = new WebResponseModel(ex);
                return StatusCode((int)exception.StatusCode, exception);
            }
        }
    }
}
