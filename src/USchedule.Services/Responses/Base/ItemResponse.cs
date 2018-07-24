namespace USchedule.Services.Responses.Base
{
    public class ItemResponse<T> : BaseResponse where T : class
    {
        public T Model { get; set; }
    }
}