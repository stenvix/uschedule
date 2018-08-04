using AutoMapper;
using USchedule.Core.Entities.Implementations;
using USchedule.Models.Domain;

namespace USchedule.Models.Profiles
{
    public class SemesterProfile: Profile
    {
        public SemesterProfile()
        {
            CreateMap<Semester, SemesterModel>();
            CreateMap<SemesterModel, Semester>();
        }
    }
}