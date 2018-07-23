using USchedule.Models.Domain.Base;

namespace USchedule.Models.Domain
{
    public class RoomModel: BaseModel
    {
        public string Number { get; set; }
        
        public BuildingModel Building { get; set; }
    }
}