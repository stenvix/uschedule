using USchedule.Models.Domain.Base;

namespace USchedule.Models.Domain
{
    public class UniversityModel : ShortTitleModel
    {
        public string Site { get; set; }
        
        public BuildingModel BuildingModel { get; set; }
    }
}