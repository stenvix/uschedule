using USchedule.Models.Domain.Base;

namespace USchedule.Models.Domain
{
    public class InstituteModel: ShortTitleModel
    {
        public BuildingModel Building { get; set; }
    }
}