using AutoMapper;
using USchedule.Core.Entities.Implementations;
using USchedule.Models.Domain;

namespace USchedule.Models.Profiles
{
    public class LessonTimeProfile : Profile
    {
        public LessonTimeProfile()
        {
            CreateMap<LessonTime, LessonTimeModel>();
            CreateMap<LessonTimeModel, LessonTime>();
        }
    }
}