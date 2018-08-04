using AutoMapper;
using USchedule.Core.Entities.Implementations;
using USchedule.Models.Domain;

namespace USchedule.Models.Profiles
{
    public class InstituteProfile: Profile
    {
        public InstituteProfile()
        {
            CreateMap<Institute, InstituteModel>();
            CreateMap<InstituteModel, Institute>();
        }
    }
}