using AutoMapper;
using USchedule.Core.Entities.Implementations;
using USchedule.Models.Domain;

namespace USchedule.Models.Profiles
{
    public class UniversityProfile: Profile
    {
        public UniversityProfile()
        {
            CreateMap<University, UniversityModel>();
            CreateMap<UniversityModel, University>();
        }
    }
}