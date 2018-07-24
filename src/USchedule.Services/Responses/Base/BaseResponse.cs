using System.Net;

namespace USchedule.Services.Responses.Base
{
    public class BaseResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}