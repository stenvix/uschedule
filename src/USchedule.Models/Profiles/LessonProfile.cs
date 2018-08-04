using AutoMapper;
using USchedule.Core.Entities.Implementations;
using USchedule.Models.Domain;

namespace USchedule.Models.Profiles
{
    public class LessonProfile: Profile
    {
        public LessonProfile()
        {
            CreateMap<Lesson, LessonModel>()
                .ForMember(dest=>dest.Teacher, src=>src.MapFrom(i=>i.TeacherSubject.Teacher))
                .ForMember(dest=>dest.Subject, src=>src.MapFrom(i=>i.TeacherSubject.Subject));
            CreateMap<LessonModel, Lesson>();
        }
    }
}