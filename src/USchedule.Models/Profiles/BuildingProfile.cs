using AutoMapper;
using USchedule.Core.Entities.Implementations;
using USchedule.Models.Domain;

namespace USchedule.Models.Profiles
{
    public class BuildingProfile: Profile
    {
        public BuildingProfile()
        {
            CreateMap<Building, BuildingModel>();
            CreateMap<BuildingModel, Building>();
        }
    }
}