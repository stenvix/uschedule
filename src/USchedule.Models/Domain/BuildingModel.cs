using USchedule.Models.Domain.Base;

namespace USchedule.Models.Domain
{
    public class BuildingModel: ShortTitleModel
    {
        public LocationModel Location { get; set; }
    }
}