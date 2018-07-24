using System.Collections.Generic;

namespace USchedule.Services.Responses.Base
{
    public class ItemsResponse<T>: BaseResponse
    {
        public IList<T> Models { get; set; }
    }
}