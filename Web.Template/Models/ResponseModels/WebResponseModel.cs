using System.Net;

namespace Web.Template.Models.ResponseModels
{
    public class WebResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public WebResponseModel() { }
        public WebResponseModel(HttpStatusCode statusCode, string? message, object? data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public WebResponseModel(string? message,object data)
        {
            StatusCode = HttpStatusCode.OK;
            Message = message;
            Data = data;
        }

        public WebResponseModel(string message)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Message = message;
            Data = null;
        }

        public WebResponseModel(Exception ex)
        {
            StatusCode = HttpStatusCode.InternalServerError;
            Message = ex.InnerException?.Message ?? ex.Message;
            Data = ex;
        }
    }
}
