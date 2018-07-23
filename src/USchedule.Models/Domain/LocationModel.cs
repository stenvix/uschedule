using USchedule.Models.Domain.Base;

namespace USchedule.Models.Domain
{
    public class LocationModel: BaseModel
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}